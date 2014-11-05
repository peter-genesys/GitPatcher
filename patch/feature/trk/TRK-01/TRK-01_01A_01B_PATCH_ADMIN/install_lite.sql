PROMPT LOG TO TRK-01_01A_01B_PATCH_ADMIN.log
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

define patch_name = 'TRK-01_01A_01B_PATCH_ADMIN'
define patch_desc = 'Install Tracker'
define patch_path = 'feature/trk/TRK-01/TRK-01_01A_01B_PATCH_ADMIN/'
SPOOL TRK-01_01A_01B_PATCH_ADMIN.log
CONNECT &&PATCH_ADMIN_user/&&PATCH_ADMIN_password@&&database
set serveroutput on;
select user||'@'||global_name Connection from global_name;


PROMPT TABLES

WHENEVER SQLERROR CONTINUE
PROMPT patch_prereqs.tab 
@&&patch_path.patch_prereqs.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

WHENEVER SQLERROR CONTINUE
PROMPT patch_supersedes.tab 
@&&patch_path.patch_supersedes.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

WHENEVER SQLERROR CONTINUE
PROMPT patches.tab 
@&&patch_path.patches.tab;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

PROMPT DATABASE LINKS

PROMPT patch_admin_backward_dblink.dblink 
@&&patch_path.patch_admin_backward_dblink.dblink;
Show error;

PROMPT patch_admin_forward_dblink.dblink 
@&&patch_path.patch_admin_forward_dblink.dblink;
Show error;

PROMPT PACKAGE SPECS

PROMPT patch_installer.pks 
@&&patch_path.patch_installer.pks;
Show error;

PROMPT patch_invoker.pks 
@&&patch_path.patch_invoker.pks;
Show error;

PROMPT patch_prereqs_tapi.pks 
@&&patch_path.patch_prereqs_tapi.pks;
Show error;

PROMPT patch_supersedes_tapi.pks 
@&&patch_path.patch_supersedes_tapi.pks;
Show error;

PROMPT patches_tapi.pks 
@&&patch_path.patches_tapi.pks;
Show error;

PROMPT text_manip.pks 
@&&patch_path.text_manip.pks;
Show error;

PROMPT VIEWS

PROMPT components_unapplied_v.vw 
@&&patch_path.components_unapplied_v.vw;
Show error;

PROMPT patch_prereqs_v.vw 
@&&patch_path.patch_prereqs_v.vw;
Show error;

PROMPT patch_supersedes_v.vw 
@&&patch_path.patch_supersedes_v.vw;
Show error;

PROMPT patches_components_v.vw 
@&&patch_path.patches_components_v.vw;
Show error;

PROMPT patches_dependency_v.vw 
@&&patch_path.patches_dependency_v.vw;
Show error;

PROMPT patches_unapplied_v.vw 
@&&patch_path.patches_unapplied_v.vw;
Show error;

PROMPT patches_unpromoted_v.vw 
@&&patch_path.patches_unpromoted_v.vw;
Show error;

PROMPT patches_v.vw 
@&&patch_path.patches_v.vw;
Show error;

PROMPT user_object_dependency_v.vw 
@&&patch_path.user_object_dependency_v.vw;
Show error;

PROMPT GRANTS

PROMPT end_user.grt 
@&&patch_path.end_user.grt;

PROMPT PACKAGE BODIES

PROMPT patch_installer.pkb 
@&&patch_path.patch_installer.pkb;
Show error;

PROMPT patch_invoker.pkb 
@&&patch_path.patch_invoker.pkb;
Show error;

PROMPT patch_prereqs_tapi.pkb 
@&&patch_path.patch_prereqs_tapi.pkb;
Show error;

PROMPT patch_supersedes_tapi.pkb 
@&&patch_path.patch_supersedes_tapi.pkb;
Show error;

PROMPT patches_tapi.pkb 
@&&patch_path.patches_tapi.pkb;
Show error;

PROMPT text_manip.pkb 
@&&patch_path.text_manip.pkb;
Show error;

PROMPT TRIGGERS

PROMPT patch_prereqs_aud.trg 
@&&patch_path.patch_prereqs_aud.trg;
Show error;

PROMPT patch_supersedes_aud.trg 
@&&patch_path.patch_supersedes_aud.trg;
Show error;

PROMPT patches_aud.trg 
@&&patch_path.patches_aud.trg;
Show error;

PROMPT patches_biur.trg 
@&&patch_path.patches_biur.trg;
Show error;

COMMIT;
COMMIT;
PROMPT 
PROMPT install_lite.sql - COMPLETED.
spool off;


COMMIT;

