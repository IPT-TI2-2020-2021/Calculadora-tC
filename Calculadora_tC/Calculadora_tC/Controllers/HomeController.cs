using Calculadora_tC.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora_tC.Controllers {
   public class HomeController : Controller {
      private readonly ILogger<HomeController> _logger;


      public HomeController(ILogger<HomeController> logger) {
         _logger = logger;
      }


      /// <summary>
      /// invocação da calculadora, em modo HttpGET
      /// </summary>
      /// <returns></returns>
      public IActionResult Index() {

         // enviar os valores iniciais para a view
         ViewBag.Visor = "0";
         ViewBag.Operador = "";
         ViewBag.PrimeiroOperando = "";
         ViewBag.LimpaVisor = true + "";

         return View();
      }


      /// <summary>
      /// apresentação da calculadora, em modo HttpPOST
      /// </summary>
      /// <param name="bt">valor do botão pressionado pelo utilizador</param>
      /// <param name="visor">valor visível no Visor da calculadora e utilizado como segundo operando na operação aritmética</param>
      /// <param name="operador">operador a ser utilizado na operação aritmética</param>
      /// <param name="primeiroOperando">primeiro operando da operação aritmética</param>
      /// <param name="limpaVisor">marca o visor para ser reiniciado</param>
      /// <returns></returns>
      [HttpPost]
      public IActionResult Index(
         string bt, 
         string visor,
         string operador,
         string primeiroOperando,
         bool limpaVisor
         ) {


         switch (bt) {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "0":
               // construir o 'número' do visor
               if (visor == "0" || limpaVisor) visor = bt;
               else visor = visor + bt;  //  visor += bt;
               // marcar que já não preciso de limpar o visor
               limpaVisor = false;
               break;

            case "+/-":
               // efetuar a inversão do valor do visor
               visor = Convert.ToDouble(visor) * -1 + "";
               //if (visor.StartsWith('-')) visor = visor.Substring(1);
               //else visor = '-' + visor;
               // marcar que já não preciso de limpar o visor
               limpaVisor = false;
               break;

            case ",":
               // processa a parte decimal do número do visor
               if (!visor.Contains(',')) visor += ',';
               // marcar que já não preciso de limpar o visor
               limpaVisor = false;
               break;

            case "+":
            case "-":
            case "x":
            case ":":
            case "=":
               // processar as operações aritméticas
               if (!string.IsNullOrEmpty(operador)) {
                  // recuperar os dados anteriormente guardados
                  double operando1 = Convert.ToDouble(primeiroOperando);
                  double operando2 = Convert.ToDouble(visor);
                  // fazer o cálculo
                  switch (operador) {
                     case "+":
                        visor = operando1 + operando2 + "";
                        break;
                     case "-":
                        visor = operando1 - operando2 + "";
                        break;
                     case "x":
                        visor = operando1 * operando2 + "";
                        break;
                     case ":":
                        visor = operando1 / operando2 + "";
                        break;
                  }
               }
               // vou guardar o operador para a px. operação
               if (bt == "=") operador = "";
               else operador = bt;
               // guardar o visor como primeiro operando
               primeiroOperando = visor;
               // marcar o visor como devendo ser reeniciado
               limpaVisor = true;

               break;

            case "C":
               // reiniciar a calculadora
               visor = "0";
               operador = "";
               primeiroOperando = "";
               limpaVisor = true;
               break;

         } // switch (bt)


         // enviar dados para View
         ViewBag.Visor = visor;
         ViewBag.Operador = operador;
         ViewBag.PrimeiroOperando = primeiroOperando;
         ViewBag.LimpaVisor = limpaVisor + "";

         return View();

      } // Index






      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error() {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}
