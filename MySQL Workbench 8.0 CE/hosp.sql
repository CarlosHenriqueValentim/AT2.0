drop database if exists hosp;
create database if not exists hosp;
use hosp;

create table if not exists paciente(
id int auto_increment not null primary key,
nome varchar(255) not null unique key,
preferencial char(1) not null,
numerofila int not null,
constraint preferencial check (preferencial in('S','N','s','n'))
);