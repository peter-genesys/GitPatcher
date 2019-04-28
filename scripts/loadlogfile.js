//loadlogfile.js

function debug(message) {
  ctx.write(message+ "\n");
}

//This script is called from the GP patch or directly from GP to load a log file into the database.
//debug("loadlogfile.js")
var patch_name  = args[3]; //patch_name, no path
debug("")
debug("Loading Log File for " + patch_name)

var helpers = {} ;

/* for complex type a java hashmap is needed for binds */
helpers.getBindMap = function(){
   var HashMap = Java.type("java.util.HashMap");
   map = new HashMap();
   return map;
};

/* create a temp blob and load it from a local to sqlcl file */
helpers.getBlobFromFile=function (fileName){
     var b = conn.createBlob();
     var out = b.setBinaryStream(1);
     var path = java.nio.file.FileSystems.getDefault().getPath(fileName);
     java.nio.file.Files.copy(path, out);
     out.flush();
 return b;
};


/* create directories if they don't exist */

var LogDir = "logs"
var LogOrgDir = LogDir +"/" + args[1]
var LogOrgDBDir = LogOrgDir +"/" + args[2]

var newDir = java.nio.file.FileSystems.getDefault().getPath(LogDir);                  
var files   = java.nio.file.Files;
if ( ! files.exists(newDir)) {
  debug("creating directory "+newDir); 
  files.createDirectory(newDir)
}

newDir = java.nio.file.FileSystems.getDefault().getPath(LogOrgDir);  
if ( ! files.exists(newDir)) {
   debug("creating directory "+newDir); 
  files.createDirectory(newDir)
}

newDir = java.nio.file.FileSystems.getDefault().getPath(LogOrgDBDir);  
if ( ! files.exists(newDir)) {
   debug("creating directory "+newDir); 
  files.createDirectory(newDir)
}

var log_file = patch_name+".log"; //assume path is not needed, local dir
 
try {
  /* load binds */
  binds = helpers.getBindMap();

  /* add more binds */
  //binds.put("path",log_file);
  binds.put("patch_name",patch_name);
 
  blob = helpers.getBlobFromFile(log_file);
  binds.put("blob",blob);

  /* exec the insert and pass binds */
  var ret = util.execute("update arm_log set log_text = :blob , log_file_status = 'LOADED' where patch_name = :patch_name and log_file_status = 'NEW'",binds);
 
  /* move log file to the dir */

  var sourcePath = java.nio.file.FileSystems.getDefault().getPath(log_file);
  var destinationPath = java.nio.file.FileSystems.getDefault().getPath(LogOrgDBDir+"/"+log_file);
 
  try {

    var CopyOption = Java.type("java.nio.file.StandardCopyOption");
    files.move(sourcePath, destinationPath, CopyOption.REPLACE_EXISTING);
    debug("Moved Log File " + log_file + " to " + LogOrgDBDir);

  } catch (err) {
  	debug("Failed to move Log File " + log_file + " to " + LogOrgDBDir+"/"+log_file);
    //moving file failed.
    err.printStackTrace();
  }
 

}
catch(err) {
  
  debug("Log File " + log_file + " is missing.")
  //debug(err.message);
  var ret = util.execute("update arm_log set log_file_status = 'MISSING' where patch_name = :patch_name and log_file_status = 'NEW'",binds);
  

}

