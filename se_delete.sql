---- delete function ----
create function st_delete(_id character varying)
returns int AS
'
begin 
	DELETE FROM public.tbl_students
	WHERE id=_id;
	if found then
		return 1;
	else
		return 0;
	end if;
end
'
language plpgsql;