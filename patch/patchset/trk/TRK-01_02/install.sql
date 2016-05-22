PROMPT LOG TO TRK-01_02.log
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

SPOOL TRK-01_02.log
CONNECT &&PATCH_ADMIN_user/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute &&PATCH_ADMIN_user..patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_02' -
 ,i_patch_type         => 'patchset' -
 ,i_db_schema          => '&&PATCH_ADMIN_user.' -
 ,i_branch_name        => 'develop' -
 ,i_tag_from           => 'TRK-01_01' -
 ,i_tag_to             => 'HEAD' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => '12c Grants' -
 ,i_patch_componants   => 'feature\trk\TRK-01\TRK-01_02A_02B_PATCH_ADMIN' -
||',feature\trk\TRK-01\TRK-01_02A_02B_ENDUSER' -
||',feature\trk\TRK-01\TRK-01_02A_02B_SYS' -
 ,i_patch_create_date  => '11-11-2014' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'N' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

PROMPT
PROMPT Checking Prerequisite patch TRK-01_01
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_02' -
,i_prereq_patch  => 'TRK-01_01' );
PROMPT Ensure Patch Admin is late enough for this patch
execute &&PATCH_ADMIN_user..patch_installer.add_patch_prereq( -
i_patch_name     => 'TRK-01_02' -
,i_prereq_patch  => 'TRK-01_01' );
select user||'@'||global_name Connection from global_name;
COMMIT;
Prompt installing PATCHES

PROMPT feature\trk\TRK-01\TRK-01_02A_02B_PATCH_ADMIN 
@feature\trk\TRK-01\TRK-01_02A_02B_PATCH_ADMIN\install.sql;
PROMPT feature\trk\TRK-01\TRK-01_02A_02B_ENDUSER 
@feature\trk\TRK-01\TRK-01_02A_02B_ENDUSER\install.sql;
PROMPT feature\trk\TRK-01\TRK-01_02A_02B_SYS 
@feature\trk\TRK-01\TRK-01_02A_02B_SYS\install.sql;
execute &&PATCH_ADMIN_user..patch_installer.patch_completed(i_patch_name  => 'TRK-01_02');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;
