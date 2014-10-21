create or replace 
trigger patch_supersedes_aud
	before insert or update or delete on PATCH_SUPERSEDES
	for each row
declare
	l_user varchar2(200) := nvl(v('app_user'), user);
	l_date date := sysdate;
	l_action varchar2(1);
begin
	-- this is a generated trigger, DO NOT make any changes to it directly, or they will be lost
	--   to make changes please edit app_config.create_audit_scripts
 
	
	if inserting then
		-- if primary is null, then populate it from the guid sequence
		if :new.PATCH_SUPERSEDES_ID is null then
			:new.PATCH_SUPERSEDES_ID := to_number(sys_guid(),'XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX');
		end if;
		
		-- populate creation audit fields
		:new.CREATED_BY := l_user;
		:new.CREATED_ON := l_date;
	end if;
	
	if updating then
		-- ensure created audit fields don't change
		:new.CREATED_BY := :old.CREATED_BY;
		:new.CREATED_ON := :old.CREATED_ON;
	end if;
	
	if inserting or updating then
		-- populate last updated audit fields
		:new.LAST_UPDATED_BY := l_user;
		:new.LAST_UPDATED_ON := l_date;
	end if;
	
 
	
end;
/

