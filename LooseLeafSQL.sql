DROP TABLE Loaned_Books;
DROP TABLE Loans;
DROP TABLE Owned_Books;
DROP TABLE Availability_Status;
DROP TABLE Loan_Status;
DROP TABLE Books;
DROP TABLE Genre;
DROP TABLE Users;
DROP TABLE Addresses;

CREATE TABLE Addresses(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	address1 NVARCHAR(1000) NOT NULL,
	address2 NVARCHAR(1000),
	city NVARCHAR(1000) NOT NULL,
	state NVARCHAR(1000) NOT NULL,
	zipcode NVARCHAR(1000) NOT NULL,
)

CREATE TABLE Users(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	addressid INT NOT NULL FOREIGN KEY REFERENCES Addresses(id),
	username NVARCHAR(255) NOT NULL,
	userpassword NVARCHAR(255) NOT NULL,
	email NVARCHAR(255) NOT NULL,
)

CREATE TABLE Genre(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	genre NVARCHAR(255) NOT NULL, 
)

CREATE TABLE Books(
	id INT NOT NULL PRIMARY KEY IDENTITY,
	title NVARCHAR(255) NOT NULL, 
	author NVARCHAR(255) NOT NULL,
	isbn NVARCHAR(255) NOT NULL,
	genreid int NOT NULL FOREIGN KEY REFERENCES Genre(id),
) 

CREATE TABLE Loan_Status(
	id INT PRIMARY KEY IDENTITY NOT NULL,
	loanStatus NVARCHAR(255) NOT NULL,
)

CREATE TABLE Availability_Status(
	id INT PRIMARY KEY IDENTITY NOT NULL,
	availabilityStatus NVARCHAR(255) NOT NULL,
)

CREATE TABLE Owned_Books(
	id INT PRIMARY KEY IDENTITY NOT NULL,
	userid INT NOT NULL FOREIGN KEY REFERENCES Users(id),
	bookid INT NOT NULL FOREIGN KEY REFERENCES Books(id),
	condition NVARCHAR(255) NOT NULL, 
	availabilityStatusId int NOT NULL FOREIGN KEY REFERENCES Availability_Status(id),
)

CREATE TABLE Loans(
	id INT PRIMARY KEY IDENTITY NOT NULL,
	userid INT NOT NULL FOREIGN KEY REFERENCES Users(id),
	owned_bookid INT NOT NULL FOREIGN KEY REFERENCES Owned_Books(id),
	message  NTEXT NOT NULL,
	loanStatusId INT NOT NULL FOREIGN KEY REFERENCES Loan_Status(id),
	ispublic BIT NOT NULL,
	dropoffdate DATETIME NOT NULL, 
	returneddate DATETIME NOT NULL, 
	isRecommended BIT NOT NULL,
)

CREATE TABLE Loaned_Books(
	id INT PRIMARY KEY IDENTITY NOT NULL,
	owned_bookid INT NOT NULL FOREIGN KEY REFERENCES Owned_Books(id),
	loanid INT NOT NULL FOREIGN KEY REFERENCES Loans(id),
)





INSERT INTO Genre (genre) VALUES
	('Action and Adventure'),
	('Children'),
	('Classics'),
	('Comic Book or Graphic Novel'),
	('Historical Fiction'),
	('Horror'),
	('Fantasy'),
	('Literary Fiction'),
	('Romance'),
	('Science Fiction'),
	('Suspense and Thrillers'),
	('Biographies and Autobiographies'),
	('History'),
	('Poetry'),
	('Self-Help'),
	('True Crime'),
	('Cookbook');

INSERT INTO addresses (address1, address2, city, state, zipcode) VALUES
	('98 Pyongyang Boulevard', null, 'Arlington', 'Texas',	42141),
	('913 Coacalco de Berriozbal Loop', null, 'Arlington', 'Texas',	42141),
	('1308 Arecibo Way', null, 'Arlington', 'Texas', 42141),
	('587 Benguela Manor', null, 'Arlington', 'Texas', 42141),
	('43 Vilnius Manor', null, 'Arlington', 'Texas', 42141),
	('660 Jedda Boulevard', null, 'Arlington', 'Texas',	42141),
	('782 Mosul Street', null, 'Arlington', 'Texas', 42141),
	('1427 Tabuk Place', null, 'Arlington', 'Texas', 42141),
	('770 Bydgoszcz Avenue', null, 'Dallas', 'Texas', 11067),
	('1666 Beni-Mellal Place', null, 'Dallas', 'Texas',	11067),
	('533 al-Ayn Boulevard', null, 'Dallas', 'Texas', 11067),
	('530 Lausanne Lane', null,	'Dallas', 'Texas', 11067),
	('32 Pudukkottai Lane', null, 'Dallas', 'Texas', 11067),
	('1866 al-Qatif Avenue', null, 'Dallas', 'Texas', 11067),
	('1135 Izumisano Parkway', null, 'Dallas', 'Texas',	11067),
	('1895 Zhezqazghan Drive', null, 'Dallas', 'Texas',	11067),
	('1894 Boa Vista Way', null, 'Dallas', 'Texas',	11067),
	('333 Goinia Way', null, 'Dallas', 'Texas',	11067),
	('369 Papeete Way', null, 'Dallas', 'Texas', 11067),
	('786 Matsue Way', null, 'Dallas', 'Texas',	11067),
	('1191 Sungai Petani Boulevard', null, 'Dallas', 'Texas', 11067),
	('793 Cam Ranh Avenue', 'Apartment B', 'Dallas', 'Texas', 11067),
	('1795 Santiago de Compostela Way', null, 'Dallas', 'Texas', 11067),
	('1214 Hanoi Way', null, 'Dallas', 'Texas',	11067),
	('401 Sucre Boulevard', null, 'Dallas', 'Texas', 11067),
	('682 Garden Grove Place', 'Apartment 103',	'Dallas', 'Texas', 11067),
	('1980 Kamjanets-Podilskyi Street', null, 'Dallas', 'Texas', 11067),
	('1936 Cuman Avenue', null,	'Dallas', 'Texas', 11067),
	('1485 Bratislava Place', null,	'Dallas', 'Texas', 11067),
	('1717 Guadalajara Lane', null,	'Dallas', 'Texas', 11067),
	('920 Kumbakonam Loop', null, 'Dallas', 'Texas', 11067),
	('1121 Loja Avenue', null, 'Dallas', 'Texas', 11067),
	('879 Newcastle Way', null,	'Dallas', 'Texas', 11067),
	('1309 Weifang Street', null, 'Dallas', 'Texas', 11067),
	('1944 Bamenda Way', null, 'Dallas', 'Texas', 11067);

INSERT INTO Availability_Status (availabilityStatus) VALUES
	('Available'),
	('Checked Out'),
	('In Process'),
	('Unknown');

INSERT INTO Loan_Status (loanStatus) VALUES
	('Requested'),
	('Approved'),
	('Denied');

INSERT INTO Users (addressid, username, userpassword, email) VALUES
	(1, 'cordagepayment', 'sbdfhgsetr@$#%#', 'bumblebeehedgehog@ymail.com'),
	(2, 'trekswarm', 'asgserr25645', 'bazookaelephant@ymail.com'),
	(3, 'granolacopy', 'FHG767dgyIGI', 'turduckencardinal@ymail.com'),
	(4, 'editionheady', 'hdtrhrt&45', 'doodlehorse@ymail.com'),
	(5, 'cantstandher', 'xsdhrt346', 'egadpeacock@ymail.com'),
	(6, 'pradabasket', 'srethrt675', 'bamboocolobus@ymail.com'),
	(7, 'molesomebody', 'lyiulyu453', 'bowyangbloodhound@ymail.com'),
	(8, 'expandwill', 'serterh7896', 'malarkeylark@ymail.com'),
	(9, 'boocarefully', 'awengh576', 'burgooswallow@ymail.com'),
	(10, 'orchestradiving', 'cfgjfyti76', 'sagyak@ymail.com'),
	(11, 'humcathead', 'serswbf57455', 'awkwardpartridge@ymail.com'),
	(12, 'middleahem', 'sehtg568', 'turnipferret@ymail.com'),
	(13, 'gameuvula', 'vgugyu453', 'baconparrot@ymail.com'),
	(14, 'gentlemanunlike', 'dthdrt7869', 'snorkelguillemot@ymail.com'),
	(15, 'uneasesituation', 'sghstr5647', 'occiputant@ymail.com'),
	(16, 'cakerowboat', 'xdhert4563', 'schnitzelcolt@ymail.com'),
	(17, 'ericssonbreeches', 'fgyk467865', 'hoipolloicoyote@ymail.com'),
	(18, 'originhomesick', 'fter4535T7I#%', 'hootenanypanda@ymail.com'),
	(19, 'emailclosed', 'dftj$^#$^gy5', 'bodaciousdonkey@ymail.com'),
	(20, 'deskgeorgian', 'fjytry6785&', 'snoolseahorse@ymail.com'),
	(21, 'gaitersbew', 'gyukyu345673', 'troglodytemandrill@ymail.com'),
	(22, 'briocheconstruct', 'ftyt3567', 'hogwashtapir@ymail.com'),
	(23, 'awarephotograph', 'drtjerjy45745', 'snoutsmelt@ymail.com'),
	(24, 'mainlyemotion', 'ftjryt467', 'kumquatruffs@ymail.com'),
	(25, 'coststriker', 'dtjer4575', 'cahootsgibbon@ymail.com'),
	(26, 'funeralprimarily', 'cghjty4563', 'rummagehawk@ymail.com'),
	(27, 'madequatorial', 'aef2345632', 'conniptionseal@ymail.com'),
	(28, 'towpetrified', 'weryw46sdf', 'maverickbison@ymail.com'),
	(29, 'accountantmustering', 'rthert4567', 'follicledoves@ymail.com'),
	(30, 'zebraconscious', 'yjrty5679', 'turdiformwaterfowl@ymail.com'),
	(31, 'producemisguided', 'sfdgjdty3457', 'codswallopibexe@ymail.com'),
	(32, 'buckpurple', 'zsfdxc346', 'squeegeewalrus@ymail.com'),
	(33, 'bedroomwhirlwind', 'ghyo2345#$', 'bubblesbaboon@ymail.com'),
	(34, 'mcdonaldsbrag', 'xdfhr568', 'cougarboa@ymail.com'),
	(35, 'resentfulgymnastics', 'bdtgdrtu4568', 'fripperyjackrabbit@ymail.com');
