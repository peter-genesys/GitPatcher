PROMPT LOG TO TRK-01_02A_02B_ENDUSER.log
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

define patch_name = 'TRK-01_02A_02B_ENDUSER'
define patch_desc = '12C Grant each enduser that GitPatcher will make patches for.'
define patch_path = 'feature/trk/TRK-01/TRK-01_02A_02B_ENDUSER/'
SPOOL TRK-01_02A_02B_ENDUSER.log
CONNECT &&ENDUSER_user/&&ENDUSER_password@&&database
set serveroutput on;
select user||'@'||global_name Connection from global_name;


PROMPT GRANTS

PROMPT patch_admin_12c.grt 
@&&patch_path.patch_admin_12c.grt;

COMMIT;
COMMIT;
PROMPT 
PROMPT install_lite.sql - COMPLETED.
spool off;


COMMIT;

