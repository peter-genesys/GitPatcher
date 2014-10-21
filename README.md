GitPatcher
==========

Oracle DB Patch Creator and Apex Version Controller

The GitPatcher is for controlled development of Oracle PL/SQL and Apex applications.  
Other componants such as SQL, Oracle Forms and Reports, Java, javascript etc can also be included.

GitPatcher is a VB.net tool (Why VB.net - mainly because i find it easy to work with, like PLSQL and unlike java).

One day i hope to migrate it from MS Visual Studio to MonoDevelop, but it's not urgent.

The DB Patch Creator is the product of many years of trying to automate the creation of database patches, 
to provide reliable and accurate patches that don't take long to create.  My first versions of this were done relying on SVN,
and implemented in DOS, then VBscript and then finally VB.net.  Now i've moved onto Git with VB.net.

Git is so much better than SVN, for reliability of branching and has great capacity for replication to multiple remote repos.

My method also employs the use of an Oracle database in a VM.  I've also been using VM's for database development for about
8 years.  Originally usingn MS VPC, but now using Oracle (formerly SUN's) Virtual Box.

Virtual Box is awesome for they way i develop because of the ability to snapshot the database.  This allows me to use a cycle
that begins with a snapshot of the DB.  I develop some code against the database.  Make some changes to APEX, which is a 
RAD GUI tool, and also in the DB.  Commit all my changes to GIT, including an export of Apex, split into hundreds of componant files.

When ready to patch, I snapshot the VM again.  So i can return if needed.  I revert to my pre-work snapshot. I run the Apex 
application back into the VM, overwriting the earlier version entirely.  I snapshot again, just in case i stuff up the patch.
 
I use the GitPatcher to create a patch, which it does by identifying all the changes I've made in Git since my last patch, 
listing them in a tree for me to choose and order my patch.  It this creates the patch, and I execute it against the DB in the VM.
This version of the DB is clean, since it is prior to the development of the work, so is an excellent test of the patch, since no 
code is pre-existing in the DB.  A quick unit test.  If anything is not properly installed, i fix the patch and re-run, possibly
reverting to the pre-work state again, just to be sure.  This process ensures that the patch will run first time on the
next DB (the DEV integration system).  The DEV integration is where all the developer code comes together.  No developer works 
directly in this database.  Apex is configured to be RUN_ONLY to prevent changes, other than by release.

The patches include a patch tracking module, that allows simple and automated comparisons of patch levels across databases.
The upshot is that the GitPatcher can tell you which patches next need to be applied, and execute them in the correct order 
without spending hours figuring it all out.  This is facilitated by database links between the databases, and uses the DEV DB 
as the reference DB for all developers private DBs in VMs.

Inter-woven with this is the control of GIT and the branched used.  Each developer works in their own feature branch.  
They will regularly rebase their work from the DEVELOP branch.  Just before patching they rebase again.  They apply any new patches
to their clean snapshot.  They apply the merged APEX.  They create their own patches. Appply and Test them.  Commit the patches.
Merge the changes back into DEVELOP and push up to the remote Origin repo.  They then apply APEX and their new patches to the
DEV DB. And perform another quick unit test.  

They are then ready for the next development cycle.

The GIT repo has a MASTER branch that tracks production, DEVELOP branch for development, as well as branched to track TEST 
and UAT or Staging or any other promotion levels you may have.

Patchsets and Minor and Major releases are created by assembling a master script to run other patches (in the correct order).
These are applied to the next DB upwards.  Hotfixes can be used to create patches at other DB levels apart from DEV, 
and the Hotfix is then migrated upwards (to PROD) and backwards (to DEV)

The end goal of this application is to make creation, releasing and maintanence of patches a breeze and a pleasure to do.






