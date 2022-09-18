---- update function ----

create or replace function st_update
(
	_id character varying,
	_firstname character varying,
	_midname character varying,
	_lastname character varying
) 
	returns int 
	language plpgsql 
	AS
'
BEGIN 
	update tbl_students SET 
		firstname=_firstname, 
		midname=_midname, 
		lastname=_lastname
	WHERE id=_id;
	if found then
		return 1;
	else
		return 0;
	end if;
end
'