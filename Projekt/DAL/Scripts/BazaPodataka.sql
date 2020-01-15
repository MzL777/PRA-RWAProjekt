create database PRARWA_Projekt
go

use PRARWA_Projekt
go

create table RazinaFizickeAktivnosti (
	IDRazinaFizickeAktivnosti int primary key identity,
	Naziv nvarchar(25) not null
)
go

insert into RazinaFizickeAktivnosti values ('Niska'), ('Umjerena'), ('Izrazita')
go

create table Spol (
	IDSpol int primary key identity,
	Naziv nvarchar(25) not null
)
go

insert into Spol values ('Muški'), ('Ženski')
go

create table TipDijabetesa (
	IDTipDijabetesa int primary key identity,
	Naziv nvarchar(25) not null
)
go

insert into TipDijabetesa values ('Tip 1'), ('Tip 2')
go

create table Korisnik (
	IDKorisnik int primary key identity,
	Ime nvarchar(127) not null,
	Prezime nvarchar(127) not null,
	Email nvarchar(127) not null unique,
	LozinkaHash binary(64) not null,
	Visina float not null,
	Tezina float not null,
	DatumRodjenja datetime not null,
	SpolID int foreign key references Spol(IDSpol) not null,
	TipDijabetesaID int foreign key references TipDijabetesa(IDTipDijabetesa) not null,
	RazinaFizickeAktivnostiID int foreign key references RazinaFizickeAktivnosti(IDRazinaFizickeAktivnosti) not null,
	Salt uniqueidentifier not null
)
go

create table TipNamirnice(
	IDTipNamirnice int primary key identity,
	Naziv nvarchar(25) not null
)
go

insert into TipNamirnice values ('Bjelanèevine'), ('Ugljikohidrati'), ('Masti')
go


create table Namirnica (
	IDNamirnica int primary key identity,
	Naziv nvarchar(50) not null,
	TipNamirniceID int foreign key references TipNamirnice(IDTipNamirnice) not null,
	EnergetskaVrijednostKcalPoGramu float not null check (EnergetskaVrijednostKcalPoGramu > 0),
	EnergetskaVrijednostKJPoGramu float not null check (EnergetskaVrijednostKJPoGramu > 0),
	Vrijedi bit not null
)
go


create table MjernaJedinica (
	IDMjernaJedinica int primary key identity,
	Naziv nvarchar(50) not null,
	Vrijedi bit not null
)
go


create table Namirnica_MjernaJedinica (
	IDNamirnicaMjernaJedinica int primary key identity,
	NamirnicaID int foreign key references Namirnica(IDNamirnica) not null,
	MjernaJedinicaID int foreign key references MjernaJedinica(IDMjernaJedinica) not null,
	TezinaUGramima int not null check (TezinaUGramima > 0)
)
go


create table NazivObroka (
	IDNazivObroka int primary key identity,
	Naziv nvarchar(50) not null,
	Vrijedi bit not null
)
go


create table KombinacijaObroka (
	IDKombinacijaObroka int primary key identity,
	VrijediOd datetime not null,
	VrijediDo datetime null
)
go


create table ObrokUKombinaciji (
	IDObrokUKombinaciji int primary key identity,
	KombinacijaObrokaID int foreign key references KombinacijaObroka(IDKombinacijaObroka) not null,
	NazivObrokaID int foreign key references NazivObroka(IDNazivObroka) not null,
	UdioBjelancevina int check (UdioBjelancevina <= 100 and UdioBjelancevina >= 0) not null,
	UdioMasti int check (UdioMasti <= 100 and UdioMasti >= 0) not null,
	UdioUgljikohidrata int check (UdioUgljikohidrata <= 100) not null,
	DnevniUdio int check (DnevniUdio <= 100 and DnevniUdio >= 0) not null,
	constraint zbroj check (UdioMasti + UdioBjelancevina + UdioUgljikohidrata = 100)
)
go


create table Jelovnik (
	IDJelovnik int primary key identity,
	KorisnikID int foreign key references Korisnik(IDKorisnik) not null,
	Datum datetime not null
)
go


create table Obrok (
	IDObrok int primary key identity,
	JelovnikID int foreign key references Jelovnik(IDJelovnik) not null,
	NazivObrokaID int foreign key references NazivObroka(IDNazivObroka) not null
)
go


create table Obrok_Namirnica (
	ObrokID int foreign key references Obrok(IDObrok) not null,
	NamirnicaID int foreign key references Namirnica(IDNamirnica) not null,
	Kolicina float check (Kolicina > 0) not null
)
go


create table Administrator (
	IDAdministrator int primary key identity,
	KorisnickoIme nvarchar(127) not null,
	LozinkaHash binary(64) not null,
	Salt uniqueidentifier not null
)
go


declare @salt uniqueidentifier = newid()
insert into Administrator (KorisnickoIme, LozinkaHash, Salt) values
('Admin', HASHBYTES('SHA2_512', 'WhosGonnaGuess'+CAST(@salt AS NVARCHAR(36))), @salt)
go

-- PROCEDURE
--administrator
create proc LoginAdministrator
    @KorisnickoIme nvarchar(127),
    @Lozinka nvarchar(100)
as
begin
    set nocount on
    declare @adminID int

    if EXISTS (select top 1 KorisnickoIme
		from Administrator
		where KorisnickoIme=@KorisnickoIme)
    begin
        set @adminID=(select IDAdministrator
			from Administrator
			where KorisnickoIme=@KorisnickoIme
			AND LozinkaHash=HASHBYTES('SHA2_512', @Lozinka+CAST(Salt AS NVARCHAR(36))))

       if(@adminID IS NULL)
           select 3 as Izlaz	--lozinka neispravna
       else 
           select 1 as Izlaz	--uspjeh
    end
    else
       select 2 as Izlaz	--korisnicko ime ne postoji
end
go


--namirnice
create proc DodajNamirnicu
	@Naziv nvarchar(50),
	@TipNamirniceID int,
	@EnergetskaVrijednostKcalPoGramu float,
	@EnergetskaVrijednostKJPoGramu float
as
begin
	insert into Namirnica values
	(@Naziv, @TipNamirniceID, @EnergetskaVrijednostKcalPoGramu, @EnergetskaVrijednostKJPoGramu, 1)
end
go

create proc UrediNamirnicu
	@ID int,
	@Naziv nvarchar(50),
	@EnergetskaVrijednostKcalPoGramu float,
	@EnergetskaVrijednostKJPoGramu float,
	@TipNamirniceID int
as
begin
	update Namirnica
	set Naziv=@Naziv,
		TipNamirniceID=@TipNamirniceID,
		EnergetskaVrijednostKcalPoGramu=@EnergetskaVrijednostKcalPoGramu,
		EnergetskaVrijednostKJPoGramu=@EnergetskaVrijednostKJPoGramu
	where IDNamirnica=@ID
end
go

create proc UkloniNamirnicu
	@ID int
as
begin
	update Namirnica
	set Vrijedi=0
	where IDNamirnica=@ID
end
go

create proc DohvatiNamirnice
as
	select n.IDNamirnica, n.Naziv, n.TipNamirniceID, n.EnergetskaVrijednostKJPoGramu, n.EnergetskaVrijednostKcalPoGramu
	from Namirnica as n
	where n.Vrijedi=1
go


--mjerne jedinice
create proc DodajMjernuJedinicu
	@Naziv nvarchar(50)
as
begin
	insert into MjernaJedinica values
	(@Naziv, 1)
end
go

create proc UrediMjernuJedinicu
	@ID int, @Naziv nvarchar(50)
as
begin
	update MjernaJedinica
	set Naziv=@Naziv
	where IDMjernaJedinica=@ID
end
go

create proc UkloniMjernuJedinicu
	@ID int
as
begin
	update MjernaJedinica
	set Vrijedi=0
	where IDMjernaJedinica=@ID
end
go

create proc DohvatiMjerneJedinice
as
begin
	select m.IDMjernaJedinica, m.Naziv
	from MjernaJedinica as m
	where m.Vrijedi=1
end
go


--mjerne jedinice za namirnicu
create proc DohvatiMjerneJediniceZaNamirnicu
	@IDNamirnica int
as
begin
	select m.IDMjernaJedinica, m.Naziv, nm.TezinaUGramima
	from MjernaJedinica as m
	left join Namirnica_MjernaJedinica as nm
	on nm.MjernaJedinicaID = m.IDMjernaJedinica
	where m.Vrijedi=1 and nm.NamirnicaID=@IDNamirnica
end
go

create proc UkloniMjernuJedinicuZaNamirnicu
	@IDNamirnica int, @IDMjernaJedinica int
as
begin
	delete from Namirnica_MjernaJedinica
	where NamirnicaID=@IDNamirnica
	and MjernaJedinicaID=@IDMjernaJedinica
end
go

create proc DodajMjernuJedinicuZaNamirnicu
	@IDNamirnica int, @IDMjernaJedinica int, @TezinaUGramima int
as
begin
	insert into Namirnica_MjernaJedinica values
	(@IDNamirnica, @IDMjernaJedinica, @TezinaUGramima)
end
go

create proc UrediMjernuJedinicuZaNamirnicu
	@IDNamirnica int, @IDMjernaJedinica int, @TezinaUGramima int
as
begin
	update Namirnica_MjernaJedinica
	set TezinaUGramima=@TezinaUGramima
	where NamirnicaID=@IDNamirnica
	and MjernaJedinicaID=@IDMjernaJedinica
end
go


--nazivi obroka
create proc DodajNazivObroka
	@Naziv nvarchar(50)
as
begin
	insert into NazivObroka values
	(@Naziv, 1)
end
go

create proc UrediNazivObroka
	@ID int, @Naziv nvarchar(50)
as
begin
	update NazivObroka
	set Naziv=@Naziv
	where IDNazivObroka=@ID
end
go

create proc UkloniNazivObroka
	@ID int
as
begin
	update NazivObroka
	set Vrijedi=0
	where IDNazivObroka=@ID
end
go

create proc DohvatiNaziveObroka
as
begin
	select IDNazivObroka, Naziv
	from NazivObroka
	where Vrijedi=1
end
go


--kombinacije
create proc DodajKombinaciju
	@VrijediOd datetime
as
begin
	insert into KombinacijaObroka values
	(@VrijediOd, null)
	select scope_identity()
end
go

create proc UkloniKombinaciju
	@IDKombinacija int, @VrijediDo datetime
as
begin
	update KombinacijaObroka
	set VrijediDo=@VrijediDo
	where IDKombinacijaObroka=@IDKombinacija
end
go

create proc DohvatiKombinaciju
	@IDKombinacija int
as
begin
	select k.IDKombinacijaObroka, k.VrijediOd, k.VrijediDo
	from KombinacijaObroka as k
	where k.IDKombinacijaObroka=@IDKombinacija
		and k.VrijediDo is null
	select o.IDObrokUKombinaciji, o.NazivObrokaID,
		o.UdioBjelancevina, o.UdioMasti, o.UdioUgljikohidrata, o.DnevniUdio
	from ObrokUKombinaciji as o
	left join KombinacijaObroka as ko
	on ko.IDKombinacijaObroka=o.KombinacijaObrokaID
	where o.KombinacijaObrokaID=@IDKombinacija
		and ko.VrijediDo is null
end
go

create proc DohvatiKombinacijuZaBrojObroka
	@BrojObroka int
as
begin
	select top 1 k.IDKombinacijaObroka, k.VrijediOd, k.VrijediDo
	from KombinacijaObroka as k
	where (VrijediDo is null
		and @BrojObroka = (
		select COUNT(*)
		from ObrokUKombinaciji as ok
		where ok.KombinacijaObrokaID=k.IDKombinacijaObroka
			and k.VrijediDo is null))

	select o.IDObrokUKombinaciji, o.NazivObrokaID,
		o.UdioBjelancevina, o.UdioMasti, o.UdioUgljikohidrata, o.DnevniUdio
	from ObrokUKombinaciji as o
	where o.KombinacijaObrokaID=(
		select top 1 ko.IDKombinacijaObroka
		from KombinacijaObroka as ko	
		where ko.VrijediDo is null
			and @BrojObroka = (
			select COUNT(*)
			from ObrokUKombinaciji as ouk
			where ouk.KombinacijaObrokaID=ko.IDKombinacijaObroka)
				and ko.VrijediDo is null)
end
go

create proc DodajObrokKombinaciji
	@IDKombinacija int,
	@IDNazivObroka int,
	@UdioBjelancevina int,
	@UdioMasti int,
	@UdioUgljikohidrata int,
	@DnevniUdio int
as
begin
begin tran
	begin try
		if ((select SUM(DnevniUdio) from ObrokUKombinaciji where KombinacijaObrokaID=@IDKombinacija) + @DnevniUdio > 100)
			throw 50000, 'Dnevni udio obroka kombinacije ne moze biti veci od 100', 1

		insert into ObrokUKombinaciji values
		(@IDKombinacija, @IDNazivObroka, @UdioBjelancevina, @UdioMasti, @UdioUgljikohidrata, @DnevniUdio)
		commit
	end try
	begin catch
		rollback
	end catch
end
go


create proc DohvatiKombinacije
as
begin
	select k.IDKombinacijaObroka, k.VrijediOd, k.VrijediDo
	from KombinacijaObroka as k
	where k.VrijediDo is null

	select o.IDObrokUKombinaciji, o.KombinacijaObrokaID, o.NazivObrokaID,
		o.UdioBjelancevina, o.UdioMasti, o.UdioUgljikohidrata, o.DnevniUdio
	from ObrokUKombinaciji as o
	left join KombinacijaObroka as ko
	on ko.IDKombinacijaObroka=o.KombinacijaObrokaID
	where ko.VrijediDo is null
end
go


--tipovi namirnica
create proc DohvatiTipoveNamirnica
as
begin
	select IDTipNamirnice, Naziv from TipNamirnice
end
go


--korisnici
create proc RegisterUser
	@Ime nvarchar(127),
	@Prezime nvarchar(127),
	@Email nvarchar(127),
	@Lozinka nvarchar(100),
	@Visina float,
	@Tezina float,
	@DatumRodjenja datetime,
	@SpolID int,
	@TipDijabetesaID int,
	@RazinaFizickeAktivnostiID int
as
begin tran
begin try
	declare @salt uniqueidentifier = newid()
	
	if(exists(select Email from korisnik where email=@Email))select 2 as Izlaz
	
	insert into Korisnik (Ime, Prezime, Email, LozinkaHash, Visina, Tezina, DatumRodjenja, SpolID, TipDijabetesaID, RazinaFizickeAktivnostiID, Salt)
	values (@Ime, @Prezime, @Email,
		HASHBYTES('SHA2_512', @Lozinka+CAST(@salt AS NVARCHAR(36))),	--lozinkaHash
		@Visina, @Tezina, @DatumRodjenja,
		@SpolID, @TipDijabetesaID, @RazinaFizickeAktivnostiID,
		@salt)
	commit
	select 1 as Izlaz	--uspjeh
end try
begin catch
	rollback
	select 0 as Izlaz	--greska
end catch
go


create proc LoginUser
    @Email nvarchar(127),
    @Lozinka nvarchar(100)
as
begin
	set nocount on
    declare @userID int

    if EXISTS (select top 1 Email
		from Korisnik
		where Email=@Email)
    begin
        set @userID=(select IDKorisnik
			from Korisnik
			where Email=@Email
			AND LozinkaHash=HASHBYTES('SHA2_512', @Lozinka+CAST(Salt AS NVARCHAR(36))))

       if(@userID IS NULL)
           select 3 as Izlaz	--lozinka neispravna
       else 
           select 1 as Izlaz	--uspjeh
    end
    else
       select 2 as Izlaz	--korisnicko ime ne postoji
end
go


create proc UrediKorisnika
	@Ime nvarchar(127),
	@Prezime nvarchar(127),
	@Email nvarchar(127),
	@Lozinka nvarchar(100),
	@Visina float,
	@Tezina float,
	@DatumRodjenja datetime,
	@SpolID int,
	@TipDijabetesaID int,
	@RazinaFizickeAktivnostiID int
as
begin
begin try
	if(exists(select IDKorisnik from Korisnik where LozinkaHash=HASHBYTES('SHA2_512', @Lozinka+CAST(Salt AS NVARCHAR(36))) and email=@Email))
	begin
		update Korisnik
		set Ime=@Ime,
			Prezime=@Prezime,
			Visina=@Visina,
			Tezina=@Tezina,
			DatumRodjenja=@DatumRodjenja,
			SpolID=@SpolID,
			TipDijabetesaID=@TipDijabetesaID,
			RazinaFizickeAktivnostiID=@RazinaFizickeAktivnostiID
		where Email=@Email
			and LozinkaHash=HASHBYTES('SHA2_512', @Lozinka+CAST(Salt AS NVARCHAR(36)))
		select 1 as Izlaz	--uspjeh
	end
	else select 3 as Izlaz
end try
begin catch
	select 2 as Izlaz	--greska
end catch
end
go


create proc DohvatiKorisnika
	@Email nvarchar(127), @Lozinka nvarchar(100)
as
begin
	set nocount on
    declare @userID int

    if EXISTS (select top 1 Email
		from Korisnik
		where Email=@Email)
    begin
        set @userID=(select IDKorisnik
			from Korisnik
			where Email=@Email
			AND LozinkaHash=HASHBYTES('SHA2_512', @Lozinka+CAST(Salt AS NVARCHAR(36))))

       if(@userID IS NULL)
           select null			--lozinka neispravna
       else 
           select IDKorisnik, Ime, Prezime, Email, Visina, Tezina, DatumRodjenja, SpolID, RazinaFizickeAktivnostiID, TipDijabetesaID
		   from Korisnik where IDKorisnik = @userID
								--uspjeh
    end
    else
       select null				--korisnicko ime ne postoji
end
go


create proc DohvatiSveKorisnike
as
begin
	select k.IDKorisnik, k.Ime, k.Prezime, k.Email, k.Visina, k.Tezina, k.DatumRodjenja, k.SpolID, k.RazinaFizickeAktivnostiID, k.TipDijabetesaID
	from Korisnik as k
end
go


create proc DohvatiSveKorisnikeDetalji
as
begin
	select k.IDKorisnik, k.Ime, k.Prezime, k.Email, k.Visina, k.Tezina, k.DatumRodjenja, s.Naziv as [Spol], r.Naziv as [RazinaFizickeAktivnosti], t.Naziv as [TipDijabetesa]
	from Korisnik as k
	left join spol as s
	on s.IDSpol = k.SpolID
	left join RazinaFizickeAktivnosti as r
	on r.IDRazinaFizickeAktivnosti = k.RazinaFizickeAktivnostiID
	left join TipDijabetesa as t
	on t.IDTipDijabetesa = k.TipDijabetesaID
end
go


--spolovi
create proc DohvatiSpolove
as
begin
	select IDSpol, Naziv
	from Spol
end
go


--tipovi dijabetesa
create proc DohvatiTipoveDijabetesa
as
begin
	select IDTipDijabetesa, Naziv
	from TipDijabetesa
end
go


--razine fizicke aktivnosti
create proc DohvatiRazineFizickeAktivnosti
as
begin
	select IDRazinaFizickeAktivnosti, Naziv
	from RazinaFizickeAktivnosti
end
go


--jelovnik
create proc DohvatiJelovnikKorisnikaZaDatum
	@KorisnikID int, @Datum datetime
as
begin
	declare @ID int
	select @ID=IDJelovnik
	from Jelovnik
	where KorisnikID=@KorisnikID
		and Datum=@Datum

	select IDJelovnik, KorisnikID, Datum
	from Jelovnik
	where IDJelovnik=@ID

	select IDObrok, JelovnikID, NazivObrokaID
	from Obrok
	where JelovnikID=@ID

	select ObrokID, NamirnicaID, Kolicina
	from Obrok_Namirnica
	where ObrokID in (select IDObrok from Obrok where JelovnikID=@ID)
end
go


create proc DodajJelovnik
	@KorisnikID int, @Datum datetime
as
begin
	insert into Jelovnik values (@KorisnikID, @Datum)
	select SCOPE_IDENTITY()
end
go


create proc DodajObrokJelovniku
	@JelovnikID int, @NazivObrokaID int
as
begin
	insert into Obrok values(@JelovnikID, @NazivObrokaID)
	select SCOPE_IDENTITY()
end
go


create proc DodajNamirnicuObroku
	@ObrokID int, @NamirnicaID int, @Kolicina float
as
begin
	insert into Obrok_Namirnica values (@ObrokID, @NamirnicaID, @Kolicina)
	select SCOPE_IDENTITY()
end
go

