---- update function ----

create or replace function st_update
(
	_id character varying,
	_name character varying,
	_alamat character varying,
	_no_handphone character varying
) 
	returns int 
	language plpgsql 
	AS
'
BEGIN 
	update tbl_students SET 
		name=_name, 
		alamat=_alamat, 
		no_handphone=_no_handphone
	WHERE id=_id;
	if found then
		return 1;
	else
		return 0;
	end if;
end
'