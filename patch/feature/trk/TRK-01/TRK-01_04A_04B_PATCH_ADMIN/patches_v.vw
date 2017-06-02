create or replace view patches_v as 
select p.*
,(select count(*) 
  from PATCH_PREREQS pr
  where pr.PATCH_NAME = p.PATCH_NAME) prereq_count --has prereq count
,(select count(*) 
  from PATCH_PREREQS pr
  where pr.PREREQ_PATCH = p.PATCH_NAME) is_prereq_count
from patches p;
