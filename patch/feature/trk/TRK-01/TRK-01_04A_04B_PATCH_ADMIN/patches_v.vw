create or replace view patches_v as 
select p.*
,(select count(*) 
  from PATCH_PREREQS pr
  where pr.PATCH_NAME = p.PATCH_NAME) prereq_count
,(select count(*) 
  from PATCH_SUPERSEDES ps
  where ps.PATCH_NAME = p.PATCH_NAME) supersedes_count
,(select count(*) 
  from PATCH_SUPERSEDES ps
  where ps.SUPERSEDES_PATCH = p.PATCH_NAME) superseded_count
from patches p;
