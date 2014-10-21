grant create view to &&patch_admin_user;

grant select on sys.dependency$ to &&patch_admin_user with grant option;

grant select on dba_objects to &&patch_admin_user with grant option;

grant create database link to &&patch_admin_user;
