create or replace view patches_components_v as 
select p.*
     , c.column_value patch_component 
from patches p 
    ,TABLE(patch_installer.patch_component_tab(p.patch_name)) c;