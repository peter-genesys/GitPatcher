--define BACKWARD_SCHEMA = 'A171872B'
--define FORWARD_SCHEMA  = 'A171872'
--patches_dependency_v - includes all installed patches in dependency order, 
--but excludes retired patches.
create or replace view patches_dependency_v as 
select * from TABLE(patch_installer.patch_dependency_tab);

grant select on patches_dependency_v to &&FORWARD_SCHEMA.;

