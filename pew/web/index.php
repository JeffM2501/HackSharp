<?PHP

include_once("config/config.php");
include_once("db.php");

function SiteDB()
{
	global $CONFIG_DATABASE_SERVER;
	global $CONFIG_DATABASE_DATABSE;
	global $CONFIG_DATABASE_USER;
	global $CONFIG_DATABASE_PASS;
	
	return DBConnect($CONFIG_DATABASE_SERVER,$CONFIG_DATABASE_DATABSE,$CONFIG_DATABASE_USER,$CONFIG_DATABASE_PASS);
}

function GetRequest( $name )
{
	if (!isset($_REQUEST[$name]))
		return FALSE;
	
	return mysql_real_escape_string($_REQUEST[$name]);
}

$db = SiteDB();

$action=GetRequest("action");

if ($action=="check")
{
	$name = GetRequest("name");
	$names = DBQuery($db,"SELECT UID FROM Users WHERE Name='$name'");
	
	if ($names == FALSE)
		echo "OK";
	else
		echo "BAD";
	exit;
}
else if ($action=="add")
{
	$name = GetRequest("name");
	$email = GetRequest("email");
	$pass1 = GetRequest("pass1");
	$pass2 = GetRequest("pass2");
	
	$names = DBQuery($db,"SELECT UID FROM Users WHERE Name='$name'");
	if ($names !== FALSE)
	{
		echo "BAD NAME";
		exit;
	}

	$emails = DBQuery($db,"SELECT UID FROM Users WHERE Email='$email'");
	if ($emails !== FALSE)
	{
		echo "BAD EMAIL";
		exit;
	}
	
	if ($pass1 != $pass2 || !$pass1)
	{
		echo "BAD PASS";
		exit;
	}
	
	$hash = mysql_real_escape_string(md5($pass1));
	$authkey = rand();
		
	if (!DBSet("INSERT INTO Users (Email, Name, PassHash, Verified, AuthKey) VALUES($email, $name, $hash, 0, $authkey)"))
	{
		echo "BAD INTERNAL";
		exit;
	}
	else
		echo "OK";
		
	$mail = "Click to verify http://www.awesomelaser.com/gauth/index.php?action=verify&key=$authkey$uid=$email";
	mail($email,"Game Auth Verification",$mail);
		
	exit;
}
else if ($action=="verify")
{
	$uid = GetRequest("email");
	$key = GetRequest("key");
		
	$ids = DBQuery($db,"SELECT UID FROM Users WHERE Email='$email'");

	if ($ids == FALSE)
	{
		echo "invalid uid";
		exit;
	}
	
	$id = $ids[0]['UID'];
	
	$keys = DBQuery($db,"SELECT AuthKey FROM Users WHERE UID=$id");
	if ($keys == FALSE)
	{
		echo "invalid uid";
		exit;
	}
	
	$authKey = $keys[0]['AuthKey'];
	
	if ($authKey != $key)
	{
		echo "invalid key";
		exit;
	}
	
	DBSetSingleField("UID", $id, "Users", "Verified", 1);
	
	echo "OK: Verified";
	exit;
}
else if ($action=="auth")
{
	$email = GetRequest("email");
	$pass1 = GetRequest("pass");
	
	$ids = DBQuery($db,"SELECT UID FROM Users WHERE Email='$email'");

	if ($ids == FALSE)
	{
		echo "BAD";
		exit;
	}
	
	$id = $ids[0]['UID'];
	
	$pass = DBQuery($db,"SELECT PassHash FROM Users WHERE UID=$id");
	if ($pass == FALSE)
	{
		echo "BAD";
		exit;
	}
	
	$pass = $pass[0]['PassHash'];
	
	$hash = mysql_real_escape_string(md5($pass1));
	
	if ($pass != $hash)
	{
		echo "BAD";
		exit;
	}
	
	$token = rand();
	
	DBSetSingleField("UID", $id, "Users", "Token", $token);
	echo "OK $id $token";
	exit;
}
else if ($action=="token")
{
	$id = GetRequest("id");
	$token = GetRequest("token");
	
	$tokens = DBQuery($db,"SELECT Token FROM Users WHERE UID=$id");

	if ($tokens == FALSE)
	{
		echo "BAD";
		exit;
	}
	
	$t = $tokens[0]['Token'];
	DBSetSingleField("UID", $id, "Users", "Token", 0);

	if ($token == $t)
		echo "OK";
	else
		echo "BAD";
	exit;
}

echo "nope";
?>