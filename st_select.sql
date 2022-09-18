---- select function ----
create function st_select()
returns table
(
	_id character varying,
	_name character varying,
	_alamat character varying,
	_no_handphone character varying
	
) 
	language plpgsql
	as
'
begin
	return query
	select id, name, alamat, no_handphone from tb_users;
end
'