//loadlogfile.js

//This script is called from the GP patch or directly from GP to load a log file into the database.

var patch_name  = args[1]; //patch_name, no path

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

var log_file = patch_name+".log"; //assume path is not needed, local dir

/* load binds */
binds = helpers.getBindMap();

/* add more binds */
binds.put("path",log_file);

blob = helpers.getBlobFromFile(log_file);

binds.put("blob",blob);
binds.put("patch_name",patch_name);

/* exec the insert and pass binds */
var ret = util.execute("update arm_log set log_text = :blob where patch_name = :patch_name",binds);

