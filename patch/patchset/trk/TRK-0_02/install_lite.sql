PROMPT LOG TO TRK-0_02.log
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

SPOOL TRK-0_02.log
Prompt installing PATCHES

PROMPT feature\trk\TRK-01\TRK-01_02A_02B_SYS 
@feature\trk\TRK-01\TRK-01_02A_02B_SYS\install_lite.sql;
PROMPT feature\trk\TRK-01\TRK-01_02A_02B_ENDUSER 
@feature\trk\TRK-01\TRK-01_02A_02B_ENDUSER\install_lite.sql;
PROMPT 
PROMPT install_lite.sql - COMPLETED.
spool off;