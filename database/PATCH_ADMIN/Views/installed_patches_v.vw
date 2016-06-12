create or replace view installed_patches_v as 
select p.*
from patches p
where success_yn = 'Y';

