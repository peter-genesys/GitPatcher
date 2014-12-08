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
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_01A_01B_PATCH_ADMIN_A' -
 ,i_patch_type         => 'feature' -
 ,i_db_schema          => '&&PATCH_ADMIN_user' -
 ,i_branch_name        => 'feature/trk/TRK-01' -
 ,i_tag_from           => 'TRK-01.01A' -
 ,i_tag_to             => 'TRK-01.01B' -
 ,i_supplementary      => 'A' -
 ,i_patch_desc         => 'Recreate database links' -
 ,i_patch_componants   => 'patch_admin_backward_dblink.dblink' -
||',patch_admin_forward_dblink.dblink' -
||',patches_unapplied_v.vw' -
||',components_unapplied_v.vw' -
||',patches_unpromoted_v.vw' -
 ,i_patch_create_date  => '12-02-2014' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => 'Use this when reconfiguring the links between databases' -
 ,i_rerunnable_yn      => 'Y' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT
PROMPT Checking Prerequisite patch TRK-01_01A_01B_PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_01A_01B_PATCH_ADMIN_A' -
,i_prereq_patch  => 'TRK-01_01A_01B_PATCH_ADMIN' );
PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_01A_01B_PATCH_ADMIN_A' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;


PROMPT DATABASE LINKS

PROMPT patch_admin_backward_dblink.dblink 
@&&patch_path.patch_admin_backward_dblink.dblink;
Show error;

PROMPT patch_admin_forward_dblink.dblink 
@&&patch_path.patch_admin_forward_dblink.dblink;
Show error;

PROMPT VIEWS

PROMPT patches_unapplied_v.vw 
@&&patch_path.patches_unapplied_v.vw;
Show error;

PROMPT components_unapplied_v.vw 
@&&patch_path.components_unapplied_v.vw;
Show error;

PROMPT patches_unpromoted_v.vw 
@&&patch_path.patches_unpromoted_v.vw;
Show error;

COMMIT;
PROMPT Compiling objects in schema PATCH_ADMIN
execute &&PATCH_ADMIN_user..patch_invoker.compile_post_patch;
--PATCH_ADMIN patches are likely to loose the session state of patch_installer, so complete using the patch_name parm.
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_01A_01B_PATCH_ADMIN_A');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;


COMMIT;

