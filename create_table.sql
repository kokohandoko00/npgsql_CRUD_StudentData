create SEQUENCE users_id start 20220001
create table tb_users 
(
	id character VARYING(100) default 'ST'|| nextval('st_id') primary key,
	name character VARYING(20),
	alamat character VARYING(20),
	no_handphone character varying(20)
)