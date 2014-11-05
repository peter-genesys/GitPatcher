PROMPT LOG TO TRK-01_01.log
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

SPOOL TRK-01_01.log
CONNECT PATCH_ADMIN/&&PATCH_ADMIN_password@&&database
set serveroutput on;
execute patch_admin.patch_installer.patch_started( -
  i_patch_name         => 'TRK-01_01' -
 ,i_patch_type         => 'patchset' -
 ,i_db_schema          => 'PATCH_ADMIN' -
 ,i_branch_name        => 'develop' -
 ,i_tag_from           => 'TRK-01.01A' -
 ,i_tag_to             => 'HEAD' -
 ,i_supplementary      => '' -
 ,i_patch_desc         => 'Full Tracker install' -
 ,i_patch_componants   => 'feature\trk\TRK-01\TRK-01_01A_01B_SYS' -
||',feature\trk\TRK-01\TRK-01_01A_01B_PATCH_ADMIN' -
 ,i_patch_create_date  => '11-05-2014' -
 ,i_patch_created_by   => 'Peter' -
 ,i_note               => '' -
 ,i_rerunnable_yn      => 'N' -
 ,i_remove_prereqs     => 'N' -
 ,i_remove_sups        => 'N' -
 ,i_track_promotion    => 'Y'); 

select user||'@'||global_name Connection from global_name;
COMMIT;
Prompt installing PATCHES

PROMPT feature\trk\TRK-01\TRK-01_01A_01B_SYS 
@feature\trk\TRK-01\TRK-01_01A_01B_SYS\install.sql;
PROMPT feature\trk\TRK-01\TRK-01_01A_01B_PATCH_ADMIN 
@feature\trk\TRK-01\TRK-01_01A_01B_PATCH_ADMIN\install.sql;
execute patch_admin.patch_installer.patch_completed(i_patch_name  => 'TRK-01_01');
COMMIT;
PROMPT 
PROMPT install.sql - COMPLETED.
spool off;
