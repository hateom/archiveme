<html>
	<head>
		<title>Add new user!</title>
		<style type="text/css">
			label {
				display: block;
			}
			input {
				display: block;
			}
			body {
				font-family: "Trebuchet MS", Arial, Helvetica;
			}
		</style>
	</head>
	<body>
<?php
require_once("dbdriver.php");

if( isset($_POST["username"]) && isset( $_POST["password"] ) && isset( $_POST["email"] ) )
{
	$username = $_POST["username"];
	$password = $_POST["password"];
	$email    = $_POST["email"];
	
	$db = new dbDriver();
	if( !$db->open() )
	{
		die( "Could not connect to the DB!" );
	}

	$md5pass = md5($password);
	$uid     = md5($username.$password);
	$query = "INSERT INTO accounts VALUES( '".$uid."', '".$username."', '".$md5pass."', '".$email."')";
	echo "<p><pre>".$query . "</pre></p>";
	$res = $db->query( $query );
	$db->release();
	if( !$res )
	{
		die( "Could not add user!" );
	}
}

?>
	<form action="./useradd.php" method="POST">
		<label for="username">Username: </label>
		<input id="username" name="username" type="text" />
		<label for="password">Password: </label>
		<input id="password" name="password" type="password" />
		<label for="email">Email: </label>
		<input id="email" name="email" type="text" />
		<input type="submit" value="OK">
	</form>
	</body>
</html>