---- select function ----
create function st_select()
returns table
(
	_id character varying,
	_firstname character varying,
	_midname character varying,
	_lastname character varying
	
) 
	language plpgsql
	as
'
begin
	return query
	select id, firstname, midname, lastname from tbl_students;
end
'