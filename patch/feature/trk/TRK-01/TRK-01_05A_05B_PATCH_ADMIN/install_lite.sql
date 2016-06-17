PROMPT LOG TO TRK-01_05A_05B_PATCH_ADMIN.log
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

define patch_name = 'TRK-01_05A_05B_PATCH_ADMIN'
define patch_desc = ''
define patch_path = 'feature/trk/TRK-01/TRK-01_05A_05B_PATCH_ADMIN/'
SPOOL TRK-01_05A_05B_PATCH_ADMIN.log
CONNECT PATCH_ADMIN/&&PATCH_ADMIN_password@&&database
set serveroutput on;
select user||'@'||global_name Connection from global_name;


PROMPT TABLES

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes.tab 
@&&patch_path.patch_supersedes.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

WHENEVER SQLERROR CONTINUE
PROMPT patches.tab 
@&&patch_path.patches.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

PROMPT PROCEDURES

PROMPT create_db_link.prc 
@&&patch_path.create_db_link.prc;
Show error;

PROMPT PACKAGE SPECS

PROMPT patch_installer.pks 
@&&patch_path.patch_installer.pks;
Show error;

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes_tapi.pks 
@&&patch_path.patch_supersedes_tapi.pks;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK
Show error;

PROMPT patch_invoker.pks 
@&&patch_path.patch_invoker.pks;
Show error;

PROMPT patch_prereqs_tapi.pks 
@&&patch_path.patch_prereqs_tapi.pks;
Show error;

PROMPT patches_tapi.pks 
@&&patch_path.patches_tapi.pks;
Show error;

PROMPT text_manip.pks 
@&&patch_path.text_manip.pks;
Show error;

PROMPT VIEWS

PROMPT installed_patches_v.vw 
@&&patch_path.installed_patches_v.vw;
Show error;

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes_v.vw 
@&&patch_path.patch_supersedes_v.vw;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK
Show error;

PROMPT patches_v.vw 
@&&patch_path.patches_v.vw;
Show error;

PROMPT PACKAGE BODIES

PROMPT patch_installer.pkb 
@&&patch_path.patch_installer.pkb;
Show error;

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes_tapi.pkb 
@&&patch_path.patch_supersedes_tapi.pkb;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK
Show error;

PROMPT patches_tapi.pkb 
@&&patch_path.patches_tapi.pkb;
Show error;

PROMPT patch_invoker.pkb 
@&&patch_path.patch_invoker.pkb;
Show error;

PROMPT patch_prereqs_tapi.pkb 
@&&patch_path.patch_prereqs_tapi.pkb;
Show error;

PROMPT text_manip.pkb 
@&&patch_path.text_manip.pkb;
Show error;

PROMPT TRIGGERS

PROMPT patches_biur.trg 
@&&patch_path.patches_biur.trg;
Show error;

COMMIT;
COMMIT;
PROMPT 
PROMPT install_lite.sql - COMPLETED.
spool off;


COMMIT;

