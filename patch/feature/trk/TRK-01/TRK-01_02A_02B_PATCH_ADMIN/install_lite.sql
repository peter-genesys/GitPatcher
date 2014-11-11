PROMPT LOG TO TRK-01_02A_02B_PATCH_ADMIN.log
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

define patch_name = 'TRK-01_02A_02B_PATCH_ADMIN'
define patch_desc = 'More grants for end user'
define patch_path = 'feature/trk/TRK-01/TRK-01_02A_02B_PATCH_ADMIN/'
SPOOL TRK-01_02A_02B_PATCH_ADMIN.log
CONNECT &&PATCH_ADMIN_user/&&PATCH_ADMIN_password@&&database
set serveroutput on;
select user||'@'||global_name Connection from global_name;


PROMPT GRANTS

PROMPT end_user.grt 
@&&patch_path.end_user.grt;

COMMIT;
COMMIT;
PROMPT 
PROMPT install_lite.sql - COMPLETED.
spool off;


COMMIT;

