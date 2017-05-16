--define BACKWARD_SCHEMA = 'A171872B'
--define FORWARD_SCHEMA  = 'A171872'
create or replace view patches_components_v as 
select p.*
     , c.column_value patch_component 
from patches p 
    ,TABLE(patch_installer.patch_component_tab(p.patch_name)) c;

grant select on patches_components_v to &&FORWARD_SCHEMA.;
