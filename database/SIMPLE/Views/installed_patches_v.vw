--define BACKWARD_SCHEMA = 'A171872B'
--define FORWARD_SCHEMA  = 'A171872'
create or replace view installed_patches_v as 
select p.*
from patches p
where success_yn = 'Y';

grant select on installed_patches_v to &&BACKWARD_SCHEMA.;

