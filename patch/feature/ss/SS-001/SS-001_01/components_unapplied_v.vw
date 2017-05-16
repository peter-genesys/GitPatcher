--define BACKWARD_SCHEMA = 'A171872B'
--define FORWARD_SCHEMA  = 'A171872'
create or replace view components_unapplied_v as 
select pu.*
      ,pc.patch_component 
from patches_unapplied_v pu
    ,&&BACKWARD_SCHEMA..patches_components_v pc
where pu.patch_name = pc.patch_name;    