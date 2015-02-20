PROMPT LOG TO TRK-01_01A_01B_PATCH_ADMIN_A.log
PROMPT .
SET AUTOCOMMIT OFF
SET AUTOPRINT ON
SET ECHO ON
SET FEEDBACK ON
SET PAUSE OFF
SET SERVEROUTPUT ON SIZE 1000000
SET TERMOUT ON
SET TRIMOUT ON
SET VERIFY ON
SET trims on pagesize 3000
SET auto off
SET verify off echo off define on
WHENEVER OSERROR EXIT FAILURE ROLLBACK
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

define patch_name = 'TRK-01_01A_01B_PATCH_ADMIN_A'
define patch_desc = 'Recreate database links'
define patch_path = 'feature/trk/TRK-01/TRK-01_01A_01B_PATCH_ADMIN_A/'
SPOOL TRK-01_01A_01B_PATCH_ADMIN_A.log
CONNECT &&PATCH_ADMIN_user/&&PATCH_ADMIN_password@&&database
set serveroutput on;
select user||'@'||global_name Connection from global_name;

PROMPT PROCEDURES

@&&patch_path.create_db_link.prc;
Show error;

PROMPT DATABASE LINKS

PROMPT patch_admin_backward_dblink.dblink 
@&&patch_path.patch_admin_backward_dblink.dblink;
Show error;

PROMPT patch_admin_forward_dblink.dblink 
@&&patch_path.patch_admin_forward_dblink.dblink;
Show error;

PROMPT VIEWS
WHENEVER SQLERROR CONTINUE
PROMPT patches_unapplied_v.vw 
@&&patch_path.patches_unapplied_v.vw;
Show error;

PROMPT components_unapplied_v.vw 
@&&patch_path.components_unapplied_v.vw;
Show error;


PROMPT patches_unpromoted_v.vw 
@&&patch_path.patches_unpromoted_v.vw;
Show error;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

execute dbms_utility.compile_schema(USER);

select 'INVALID '||object_type||': '||owner ||'.' || OBJECT_NAME "Invalid Objects"
from sys.dba_objects 
where owner like UPPER(USER)
and status ='INVALID';

COMMIT;
COMMIT;
PROMPT 
PROMPT install_lite.sql - COMPLETED.
spool off;


COMMIT;

