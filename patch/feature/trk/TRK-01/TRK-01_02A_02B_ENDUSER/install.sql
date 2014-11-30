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
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_02A_02B_ENDUSER' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => '&&ENDUSER_user' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.02A' -
 ,i_tag_to             => 'TRK-01.02B' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => '12C Grant each enduser that GitPatcher will make patches for.' -
 ,i_patch_componants   => 'patch_admin_12c.grt' -
 ,i_patch_create_date  => '11-11-2014' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => 'Except for SYS' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT
PROMPT Checking Prerequisite patch TRK-01_01A_01B_PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_02A_02B_ENDUSER' -
,i_prereq_patch  => 'TRK-01_01A_01B_PATCH_ADMIN' );
PROMPT
PROMPT Checking Prerequisite patch TRK-01_01A_01B_SYS
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_02A_02B_ENDUSER' -
,i_prereq_patch  => 'TRK-01_01A_01B_SYS' );
PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_02A_02B_ENDUSER' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;


PROMPT GRANTS

PROMPT patch_admin_12c.grt 
WHENEVER SQLERROR CONTINUE
@&&patch_path.patch_admin_12c.grt;
WHENEVER SQLERROR EXIT FAILURE ROLLBACK

COMMIT;
PROMPT Compiling objects in schema ENDUSER
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
execute &&PATCH_ADMIN_user..patch_installer.patch_completed;
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

