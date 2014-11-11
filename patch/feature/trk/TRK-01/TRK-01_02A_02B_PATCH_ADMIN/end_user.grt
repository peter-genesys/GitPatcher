grant execute on patch_installer to &&ENDUSER_USER;

grant execute on patch_invoker to &&ENDUSER_USER;

grant select on user_object_dependency_v to &&ENDUSER_USER;

grant select on patches_dependency_v to &&ENDUSER_USER;

grant select on patches_unpromoted_v to &&ENDUSER_USER;

grant select on patches_unapplied_v to &&ENDUSER_USER;

grant select on patches_components_v to &&ENDUSER_USER;

grant select on components_unapplied_v to &&ENDUSER_USER;

grant select on patches_v to &&ENDUSER_USER;

grant select on patch_prereqs_v to &&ENDUSER_USER;

grant select on patch_supersedes_v to &&ENDUSER_USER;