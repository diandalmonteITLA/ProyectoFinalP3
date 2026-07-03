# Para equipo A y B: 
- Jira utiliza como ID "SCRUM - N", entonces cuando lean SCRUM-15, por ejemplo, sepan que estoy hablando de algun item (historia, tarea, epica, etc...) cuyo ID es SCRUM-15. 
Para acortar un poco lo resumiré a SN, por lo que "SCRUM-15" seria "S15"

- Dentro de cada historia de usuario hay subtareas, cada subtarea tiene descripciones. Estas subtareas son las que estaran realizando.  
  
- Dicho esto, de las historias S13, S14 y S15 el Modelado de entidades y Mapeo EF Core me tocan a mi, sus tareas empiezan a partir del Mapeo EF Core.<br><br>

### Desarollo de la app:
- Para estar todos en la misma pagina, la arquitectura a utilizar es  Onion, y la convencion de nomenclatura sera en ingles, que es el estandar en programacion. Es decir, todas las clases, metodos y variables deben estar en ingles.
Solo los datos que se mostraran en la interfaz deben estar en español. Por ejemplo alertas y mensajes de error.

- Tienen este repositorio, inicialmente con las entidades definidas y las interfaces de las clases Repository(que acceden a la BD), las cuales utilizaran mediante inversion de dependencias. Eso debe ser suficiente para empezar el desarrollo.

- Cuando empiecen el desarrollo de una historia, en el tablero de Jira, muevan la tarjeta a la columna "En Curso", y cuando inicien una tarea dentro de la historia, cambien el estado de "Por Hacer" a "En Curso" y asi respectivamente.

- Tienen el link a Github, trabajen remotamente asegurandose de realizar git pull y probar antes de abrir un pull request. Esto se hace para asegurarse que todo el codigo esta al dia con el repositorio remoto y facilitar la insercion de su funcionalidad agregada sin errores de merge.
  Solo abran pull requests cuando finalicen y prueben la parte completa que les tocaba hacer (ya sea la subtarea o la historia, eso depende de ustedes).  

- Las clases Service van en la capa Application, las clases Controller en la capa Presentation (dentro del proyecto Web)<br><br>

## Puede que se me haya pasado algo, cualquier duda pregunten.
