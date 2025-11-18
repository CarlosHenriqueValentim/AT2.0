drop database if exists H;
create database if not exists H;
use H;

drop table if exists pacientes;
create table if not exists pacientes (
id int not null auto_increment primary key,
nome varchar(255) not null unique key,
preferencial boolean not null,
numerofila int not null
);
