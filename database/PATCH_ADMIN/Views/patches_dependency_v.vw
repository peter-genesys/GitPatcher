--patches_dependency_v - includes all installed patches in dependency order, 
--but excludes retired patches.
create or replace view patches_dependency_v as 
select * from TABLE(patch_installer.patch_dependency_tab);

