<?php
/*Invocar la biblioteca: */ require_once "lib/nusoap.php";  
function Insert_Ingredient($name, $cost, $n, $nmin){
    $con = mysqli_connect("localhost","root","","besmart") or die("No se pudo establecer la conexión");   
    //Si nos conectamos correctamente:
    if ($con)
    {
        $prepared_st = $con->prepare("INSERT INTO ingredient (name_ing, cost_ing, n_ing, nmin_ing) VALUES (?, ?, ?, ?)");
        $prepared_st->bind_param("i", $name);
        $prepared_st->bind_param("j", $cost);
        $prepared_st->bind_param("k", $n);
        $prepared_st->bind_param("l", $nmin);
        //Ejecutamos la parametrización:
        $prepared_st->execute();
        //Cerramos la conexión:
        mysqli_close($con);
    }
}
//Creamos nuevo objeto de tipo soap_server:
$server = new soap_server();
//Creamos nuevo objeto para wsdl:
$server->configureWSDL('Insert_Ingredient', 'haa:Insert_Ingredient');
//Registramos el(los) métodos creados (con especificación de wsdl):
$server->register("Insert_Ingredient", 
                    array('name' => 'xsd:int'), 
                    array('cost' => 'xsd:double'), 
                    array('n' => 'xsd:int'),
                    array('nmin' => 'xsd:int'),
                    'xsd:Insert_Ingredient');
//Estar a la escucha de los datos POST (para procesarlos):
$server->service(isset($HTTP_RAW_POST_DATA) ?
    $HTTP_RAW_POST_DATA : '');

?>
