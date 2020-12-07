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
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_users PRIMARY KEY (user_id)
);

CREATE TABLE profiles (
	profile_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	first_name varchar(100) NOT NULL,
	last_name varchar(100) NOT NULL,
	bio varchar(1000) NOT NULL,
	prof_location_zipcode int NOT NULL,
	prof_location_city varchar (100) NOT NULL,
	prof_location_state varchar (50) NOT NULL,
	contact_email varchar (200) NOT NULL,
	CONSTRAINT PK_profiles PRIMARY KEY (profile_id),
	CONSTRAINT FK_profiles FOREIGN KEY (user_id) REFERENCES users (user_id)
);

CREATE TABLE organizations(
	org_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	org_name varchar (100) NOT NULL,
	org_bio varchar (1000) NOT NULL,
	org_location_zipcode int NOT NULL,
	org_location_city varchar (100) NOT NULL,
	org_location_state varchar (50) NOT NULL,
	contact_email varchar (200) NOT NULL,
	CONSTRAINT PK_organizations PRIMARY KEY (org_id),
	CONSTRAINT FK_organizations_user_id FOREIGN KEY (user_id) REFERENCES users (user_id)
);

CREATE TABLE projects (
	proj_id int IDENTITY(1,1) NOT NULL,
	org_id int NULL,
	proj_name varchar(100) NOT NULL,
	proj_desc varchar(1000) NOT NULL,
	proj_location_zipcode int NOT NULL,
	proj_location_city varchar (100) NOT NULL,
	proj_location_state varchar (50) NOT NULL,
	proj_working_hours int NOT NULL,
	point_of_contact int NOT NULL,
	CONSTRAINT PK_projects PRIMARY KEY (proj_id),
	CONSTRAINT FK_projects_org_id FOREIGN KEY (org_id) REFERENCES organizations (org_id)
);

CREATE TABLE teams(
	team_id int IDENTITY(1,1) NOT NULL,
	team_name varchar (100) NOT NULL,
	team_bio varchar (1000) NOT NULL,
	team_location_zipcode int NOT NULL,
	team_location_city varchar (100) NOT NULL,
	team_location_state varchar (50) NOT NULL,
	team_contact int NOT NULL,
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
	CONSTRAINT pk_profiles_projects_project_id_user_id PRIMARY KEY (profile_id, project_id),
	CONSTRAINT fk_profiles_projects_profile_id FOREIGN KEY (profile_id) REFERENCES profiles (profile_id),
	CONSTRAINT fk_profiles_projects_project_id FOREIGN KEY (project_id) REFERENCES projects (proj_id)
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
)


--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

GO