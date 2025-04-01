// function filterLeads() {
//     let searchName = document.getElementById("searchName").value;
//     let searchPhone = document.getElementById("searchPhone").value;
//     let searchEmail = document.getElementById("searchEmail").value;
//
//     let url = `/Leads/Search?name=${encodeURIComponent(searchName)}&phone=${encodeURIComponent(searchPhone)}&email=${encodeURIComponent(searchEmail)}`;
//
//     fetch(url, {
//         method: 'GET',
//         headers: {
//             'Content-Type': 'application/json'
//         }
//     })
//         .then(response => response.json())
//         .then(data => {
//             let tableBody = document.querySelector("#leadsTable tbody");
//             tableBody.innerHTML = ""; // Limpa os resultados anteriores
//
//             data.forEach(lead => {
//                 let row = `
//                     <tr>
//                         <td>${lead.id}</td>
//                         <td>${lead.nome}</td>
//                         <td>${lead.telefone}</td>
//                         <td>${lead.email}</td>
//                         <td>${lead.cursoInteresse}</td>
//                         <td>
//                             <a class="btn btn-danger">Apagar</a>
//                             <a class="btn btn-warning">Editar</a>
//                         </td>
//                     </tr>`;
//                 tableBody.innerHTML += row;
//             });
//         });
// }
