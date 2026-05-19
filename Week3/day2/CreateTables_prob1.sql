CREATE DATABASE EventDb 
USE EventDB
CREATE TABLE UserInfo(EmailId VARCHAR(50) PRIMARY KEY,UserName VARCHAR(50) Not Null,
Role varchar(10) Not NUll check(Role IN ('Admin','Partcipant')),Password VARCHAR (21) Not Null,
CONSTRAINT CHK_UserName_Length CHECK (LEN(UserName) >= 1 AND LEN(UserName)<= 50),
CONSTRAINT CHK_Password_Length CHECK (LEN(Password) >= 6 AND LEN(Password)<= 20));


CREATE TABLE EventDetails(
    EventId INT PRIMARY KEY,
    EventName VARCHAR(50) NOT NULL,
    EventCategory VARCHAR(50) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description VARCHAR(100) NULL,
    Status VARCHAR(21) CHECK(Status IN ('Active', 'In-Active'))
);


CREATE TABLE SpeakersDetails (
    SpeakerId INT PRIMARY KEY,
    SpeakerName VARCHAR(50)NOT NULL
);

CREATE TABLE SessionInfo(
    SessionId INT PRIMARY KEY,
    EventId INT NOT NULL,
    SessionTitle VARCHAR(50) NOT NULL,
    SpeakerId INT NOT NULL,
    Description VARCHAR(100) NULL,
    SessionStart DATETIME NOT NULL,
    SessionEnd DATETIME NOT NULL,
    SessionUrl VARCHAR(255),

    CONSTRAINT FK_Session_Event FOREIGN KEY(EventId) 
        REFERENCES EventDetails(EventId),
    CONSTRAINT FK_Session_Speaker FOREIGN KEY(SpeakerId) 
        REFERENCES SpeakersDetails(SpeakerId)
);

CREATE TABLE ParticipantEventDetails(
    Id INT PRIMARY KEY,
    ParticipantEmailId VARCHAR(100) NOT NULL,
    EventId INT NOT NULL,
    SessionId INT NOT NULL,
    IsAttended BIT NOT NULL,


    CONSTRAINT FK_PED_Event FOREIGN KEY(EventId) 
        REFERENCES EventDetails(EventId),
    CONSTRAINT FK_PED_Session FOREIGN KEY(SessionId) 
        REFERENCES SessionInfo(SessionId),
      CONSTRAINT CHK_IsAttended CHECK(IsAttended IN (0,1))
);


