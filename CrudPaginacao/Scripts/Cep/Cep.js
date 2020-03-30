$(document).ready(function () {

    $("input[name = 'Cep']").change(function () {

        var cep_code = $(this).val();
        if (cep_code.length <= 0) return;


        $.getJSON("https://apps.widenet.com.br/busca-cep/api/cep.json?", { code: cep_code }, function (result) {


            if (result.status != 200) {


                alert(alert.result || "Ops' Cep informado não encontrado !");
                return;

            }
            //Atualiza os campos com os valores da consulta.
            $("input[name='Cep']").val(result.code);
            $("input[name='Estado']").val(result.state);
            $("input[name='Cidade']").val(result.city);
            $("input[name='Bairro']").val(result.district);
            $("input[name='Rua']").val(result.address);
            $("input[name='Uf']").val(result.state);

        });
    });





});



