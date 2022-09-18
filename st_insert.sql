---- insert function ----
create function st_insert
(
	_name character varying,
	_alamat character varying,
	_no_handphone character VARYING
)
returns int AS
'
begin 
	insert into public.tb_users
	(
		name,
		alamat,
		no_handphone
	)
	values 
	(
		_name,
		_alamat,
		_no_handphone
	);
	if found then
		return 1;
	else
		return 0;
	end if;
end
'
language plpgsql;