CREATE OR REPLACE TRIGGER PATCHES_BIUR 
BEFORE INSERT OR UPDATE ON PATCHES 
REFERENCING OLD AS OLD NEW AS NEW 
FOR EACH ROW 
BEGIN
  IF :OLD.log_datetime = :NEW.log_datetime THEN
    --Allows changes to metadata, when not actually running a patch.
    NULL;
  ELSE
    IF UPDATING AND :OLD.success_YN = 'Y' AND :NEW.rerunnable_yn = 'N' THEN
      RAISE_APPLICATION_ERROR(-20000,'THIS PATCH HAS BEEN RUN SUCCESSFULLY AND IS NOT FOR RERUN!');   
    END IF;
    IF UPDATING AND :OLD.success_YN = 'Y' AND :NEW.rerunnable_yn = 'Y' AND patch_installer.is_prereq_patch (
      i_prereq_patch => :OLD.patch_name ) THEN
      RAISE_APPLICATION_ERROR(-20000,'THIS PATCH WAS A PREREQ PATCH.  IT IS NOT SAFE TO RERUN!');   
    END IF;
    :NEW.install_log := 'Logged '||TO_CHAR(:NEW.log_datetime,'DD-MON-YYYY')||chr(10)
              ||:NEW.install_log||chr(10)
              ||:OLD.install_log;
  END IF;
 
END;
/
show error;

 
