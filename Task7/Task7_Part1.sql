create database Task7
go

use Task7
go

create schema Cinema
go

create table Cinema.actor(
act_id integer identity(1,1) primary key,
act_fname varchar(20),
act_lname varchar(20),
act_gender varchar(1)
)


create table Cinema.director(
dir_id int identity(1,1) primary key,
dir_fname varchar(20),
dir_lname varchar(20)
)

create table Cinema.movie(
mov_id int identity(1,1) primary key,
mov_title varchar(50),
mov_year int,
mov_time int,
mov_lang varchar(50),
mov_dt_rel datetime2,
mov_rel_country varchar(5)
)

create table Cinema.movie_cast(
act_id int references Cinema.actor(act_id),
mov_id int references Cinema.movie(mov_id),
role char(30),
primary key(act_id,mov_id)
)

create table Cinema.movie_direction(
dir_id int references Cinema.director(dir_id),
mov_id int references Cinema.movie(mov_id),
primary key(dir_id,mov_id)
)

create table Cinema.reviewer(
rev_id int identity(1,1) primary key,
rev_name varchar(30)
)

create table Cinema.genres(
gen_id int identity(1,1) primary key,
gen_title varchar(20)
)

create table Cinema.movie_genres(
mov_id int references Cinema.movie(mov_id),
gen_id int references Cinema.genres(gen_id),
Primary key(mov_id,gen_id)
)

create table Cinema.rating(
mov_id int references Cinema.movie(mov_id),
rev_id int references Cinema.reviewer(rev_id),
rev_stars tinyint,
num_o_ratings int,
primary key(mov_id,rev_id)
)