create or replace view components_unapplied_v as 
select pu.*
      ,pc.patch_component 
from patches_unapplied_v pu
    ,patches_components_v@PATCH_ADMIN_BACKWARD_DBLINK pc
where pu.patch_name = pc.patch_name;    