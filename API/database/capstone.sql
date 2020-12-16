USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	is_organization bit NULL,
	CONSTRAINT PK_users PRIMARY KEY (user_id)
);

CREATE TABLE profiles (
	profile_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	first_name varchar(100) NOT NULL,
	last_name varchar(100) NOT NULL,
	prof_image varchar(200) NULL,
	bio varchar(1000) NOT NULL,
	prof_zipcode int NOT NULL,
	prof_city varchar (100) NOT NULL,
	prof_state varchar (50) NOT NULL,
	prof_contact_email varchar (200) NOT NULL,
	CONSTRAINT PK_profiles PRIMARY KEY (profile_id),
	CONSTRAINT FK_profiles FOREIGN KEY (user_id) REFERENCES users (user_id)
);

CREATE TABLE organizations(
	org_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	org_name varchar (100) NOT NULL,
	org_image varchar (200) NOT NULL,
	org_bio varchar (1000) NOT NULL,
	org_zipcode int NOT NULL,
	org_city varchar (100) NOT NULL,
	org_state varchar (50) NOT NULL,
	org_contact_email varchar (200) NOT NULL,
	CONSTRAINT PK_organizations PRIMARY KEY (org_id),
	CONSTRAINT FK_organizations_user_id FOREIGN KEY (user_id) REFERENCES users (user_id)
);

CREATE TABLE projects (
	proj_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	proj_name varchar(100) NOT NULL,
	proj_desc varchar(1000) NOT NULL,
	proj_image varchar (200) NOT NULL,
	proj_zipcode int NOT NULL,
	proj_city varchar (100) NOT NULL,
	proj_state varchar (50) NOT NULL,
	proj_working_hours int NOT NULL,
	proj_contact_email varchar (200) NOT NULL,
	CONSTRAINT PK_projects PRIMARY KEY (proj_id),
	
);

CREATE TABLE events (
	event_id int IDENTITY(1,1) NOT NULL,
	proj_id int NOT NULL,
	event_name varchar(100) NOT NULL,
	event_desc varchar(1000) NOT NULL,
	event_zipcode int NOT NULL,
	event_city varchar (100) NOT NULL,
	event_state varchar (50) NOT NULL,
	event_time time NOT NULL,
	event_date datetime NOT NULL,
	event_working_hours int NOT NULL,
	event_contact_email varchar (200) NOT NULL,
	CONSTRAINT PK_events PRIMARY KEY (event_id),
	CONSTRAINT FK_events_proj_id FOREIGN KEY (proj_id) REFERENCES projects (proj_id)
);


CREATE TABLE teams(
	team_id int IDENTITY(1,1) NOT NULL,
	team_name varchar (100) NOT NULL,
	team_bio varchar (1000) NOT NULL,
	team_image varchar (200) NOT NULL,
	team_zipcode int NOT NULL,
	team_city varchar (100) NOT NULL,
	team_state varchar (50) NOT NULL,
	team_contact_email varchar (200) NOT NULL,
	CONSTRAINT PK_teams PRIMARY KEY (team_id),
);

CREATE TABLE causes (
	cause_id int IDENTITY(1,1) NOT NULL,
	cause_name varchar (30) NOT NULL,
	CONSTRAINT PK_causes PRIMARY KEY (cause_id),
);

CREATE TABLE profiles_projects (
	profile_id int NOT NULL,
	project_id int NOT NULL,
	hours_worked int DEFAULT 0 NOT NULL,
	CONSTRAINT pk_profiles_projects_project_id_user_id PRIMARY KEY (project_id, profile_id),
	CONSTRAINT fk_profiles_projects_project_id FOREIGN KEY (project_id) REFERENCES projects (proj_id),
	CONSTRAINT fk_profiles_projects_profile_id FOREIGN KEY (profile_id) REFERENCES profiles (profile_id)
);

CREATE TABLE profiles_teams (
	team_id int NOT NULL,
	profile_id int NOT NULL,
	CONSTRAINT pk_profiles_teams_profiles_teams PRIMARY KEY (team_id, profile_id),
	CONSTRAINT fk_profiles_teams_team_id FOREIGN KEY (team_id) REFERENCES teams (team_id),
	CONSTRAINT fk_profiles_teams_profile_id FOREIGN KEY (profile_id) REFERENCES profiles (profile_id)
);

CREATE TABLE organizations_causes(
	org_id int NOT NULL,
	cause_id int NOT NULL,
	CONSTRAINT pk_organizations_causes_oranizations_causes PRIMARY KEY (org_id, cause_id),
	CONSTRAINT fk_organizations_causes_org_id FOREIGN KEY (org_id) REFERENCES organizations (org_id),
	CONSTRAINT fk_organizations_causes_cause_id FOREIGN KEY (cause_id) REFERENCES causes (cause_id)
);
CREATE TABLE profiles_causes (
	profile_id int NOT NULL,
	cause_id int NOT NULL,
	CONSTRAINT pk_profiles_causes_profiles_causes PRIMARY KEY (profile_id, cause_id),
	CONSTRAINT fk_profiles_causes_profile_id FOREIGN KEY (profile_id) REFERENCES profiles (profile_id),
	CONSTRAINT fk_profiles_causes_cause_id FOREIGN KEY (cause_id) REFERENCES causes (cause_id)
);
CREATE TABLE teams_causes (
	team_id int NOT NULL,
	cause_id int NOT NULL,
	CONSTRAINT pk_teams_causes_teams_causes PRIMARY KEY (team_id, cause_id),
	CONSTRAINT fk_teams_causes_team_id FOREIGN KEY (team_id) REFERENCES teams (team_id),
	CONSTRAINT fk_teams_causes_cause_id FOREIGN KEY (cause_id) REFERENCES causes (cause_id)
);

CREATE TABLE projects_causes (
	proj_id int NOT NULL,
	cause_id int NOT NULL,
	CONSTRAINT pk_projects_causes_projects_causes PRIMARY KEY (proj_id, cause_id),
	CONSTRAINT fk_projects_causes_proj_id FOREIGN KEY (proj_id) REFERENCES projects (proj_id),
	CONSTRAINT fk_projects_causes_cause_id FOREIGN KEY (cause_id) REFERENCES causes (cause_id)
)


--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO causes (cause_name) VALUES ('Animals');
INSERT INTO causes (cause_name) VALUES ('Arts and Culture');
INSERT INTO causes (cause_name) VALUES ('Children');
INSERT INTO causes (cause_name) VALUES ('Community');
INSERT INTO causes (cause_name) VALUES ('Crisis Support');
INSERT INTO causes (cause_name) VALUES ('Disaster Relief');
INSERT INTO causes (cause_name) VALUES ('Education and Literacy');
INSERT INTO causes (cause_name) VALUES ('Emergency');
INSERT INTO causes (cause_name) VALUES ('Environment');
INSERT INTO causes (cause_name) VALUES ('Faith Based');
INSERT INTO causes (cause_name) VALUES ('Family Support');
INSERT INTO causes (cause_name) VALUES ('Health and Medicine');
INSERT INTO causes (cause_name) VALUES ('Housing');
INSERT INTO causes (cause_name) VALUES ('Hunger');
INSERT INTO causes (cause_name) VALUES ('Legal');
INSERT INTO causes (cause_name) VALUES ('Mental Health');
INSERT INTO causes (cause_name) VALUES ('Seniors');
INSERT INTO causes (cause_name) VALUES ('Special Needs');
INSERT INTO causes (cause_name) VALUES ('Sports and Recreation');
INSERT INTO causes (cause_name) VALUES ('Veterans');
INSERT INTO causes (cause_name) VALUES ('Women');

INSERT INTO profiles (user_id, first_name, last_name, bio, prof_zipcode, prof_city, prof_state, prof_contact_email) VALUES (1, 'Mike', 'Morel', 'Lorem ipsum ipsofacto wingardium leviosa', 44113, 'Cleveland', 'OH', 'mmorel@gmail.com')

GO