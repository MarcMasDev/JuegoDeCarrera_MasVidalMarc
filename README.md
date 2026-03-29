# 1\. Descripción del proyecto

#### *Aviso legal*

Para realizar el proyecto se han usado assets externos:

City Street Skyboxes Vol. 1 (Moodware, 2019).

NYC-like City Buildings Set (PBR) (Macrin, 2023).

Pixel Art UI Essentials (Devink, 2025).

Traffic Control Pack1 (Kuroatsu, 2012).

Procedural Terrain Painter (Staggart, 2025).

StampIT! Collection (Rowlan, 2025).

EasyRoads3D (AndaSoft, 2024).

Conifers \[BOTD] (Forst, 2024).





#### **Pec 01 - Juego de carreras.**

Realizado por Marc Mas Vidal el 29/03/26 para la asignatura Programación 3D.



###### VÍDEO: https://drive.google.com/file/d/1iJayHglAA6uVehwxj2aqAJdCqTBwlHcD/view?usp=sharing



Esta es una demo de un juego de carreras. Para probarlo hay que:

Abrir la escena “MainMenu”.

Empezar el juego usando el menú de Unity.

Seleccionar las opciones básicas usando el menú (mapa, coche y enemigos).

Darle a empezar.



\---



### **2. Controles**



Los controles son los siguientes:



Acelerar: W.

Frenar / marcha atrás: S.

Girar: A (izquierda), D (derecha).

Turbo: espacio.

R: volver al último checkpoint.

Pausa: escape.



\---



### **3. Trabajo realizado**



##### 3.1 Estructura del circuito



Se han creado un total de 2 circuitos. Uno combina ciudad y montaña, otro es más de montaña pero también incluye elementos de obra.



Incluye montañas, agua, árboles, props, edificios, carreteras... Los circuitos dejan paso a la carretera, sin árboles u otros elementos, además, la carretera sigue un terreno con deformaciones, subidas, bajadas... Además, se incluyen un total de 3 atajos marcados por elementos de obra que se pueden apartar colisionando con el coche, dos en el mapa de ciudad y uno en el mapa de montaña.



El primero en el mapa de montaña al final. Este aunque no supone un reto pasar por él, sí dispone de riesgo dado que al ser montañoso es complicado cruzarlo.



El segundo atajo es recortando el edificio después de la primera recta del mapa de ciudad.



El tercer atajo se encuentra en el mapa de ciudad al final



Además, se ha añadido un sistema de “checkpoints” para tener control de las vueltas, del tiempo transcurrido y de la diferencia con el fantasma.



Con este punto, se cumple:

El punto opcional 1: “crear diferentes pistas”

El punto opcional 8: “colocar diferentes atajos en las pistas”.



\---



#### 3.2 Manejo del coche



Para ello se ha aplicado el controlador que provenía de la plantilla de Unity. Se han modificado los valores de varias variables para que el manejo sea agradable dentro del contexto de que las carreteras del circuito son estrechas y de distancias cortas.



Hay 3 tipos de coche, cada uno con su aceleración y velocidad máxima, aplicando el principio de riesgo recompensa.



Se ha añadido un contador de velocidad.



Además, el jugador dispone de una mecánica nueva con UI responsiva: El Turbo.



Al presionar espacio, el jugador acelera el coche con fuerza. Incluyendo:

Un sistema de partículas de fuego.

Un contador en la UI con un porcentaje y un slider radial.

La UI cambia de color cuando se usa o mientras se usa (solo usable si se empieza a usar con el máximo cargado).

Al gastarse, el turbo se regenera con el tiempo hasta un máximo del 100%.



Se ha añadido también un sistema de vuelta atrás en el tiempo. Al quedarse atascado, el jugador puede presionar R para volver al último punto de control.



Si el jugador queda parado por un tiempo, se le avisa mediante un mensaje de UI animado.



Con este punto se han cumplido los siguientes puntos optativos:

El punto opcional 2: “crear diferentes coches” con un total de 3.



Opcionales extra:

Sistema de turbo.

Sistema de retroceder al punto de control.

Visualizadores en UI.



\---



### 1.3 Guardado y visualización de fantasmas



El jugador guarda su fantasma si realiza su mejor vuelta en un circuito.



La script “GhostRecorder” se encarga de guardar los datos en un serializable object de tipo “GhostLapData”.



El sistema de grabación utiliza la distancia para optimizar el número de datos almacenados, registrando únicamente cuando el vehículo se ha desplazado una distancia mínima.



Existen un total de 2 objetos serializados (uno por circuito), que guardan los datos de la mejor carrera del jugador. Al finalizar la carrera, si el tiempo es el mejor registrado, los datos actuales sobrescriben al mejor fantasma almacenado.



Además, el sistema está basado en eventos, reaccionando a:

Checkpoints alcanzados

Vueltas completadas

Fin de carrera



Esto permite registrar tiempos de forma precisa y mostrarlos a la UI en un futuro.



\---



### 1.4 Carrera de fantasmas



En las siguientes partidas, el fantasma se reproducirá, de color azul semitransparente, usando la script “GhostPlayer”.



El movimiento del fantasma se basa en los datos almacenados previamente, interpolando entre posiciones y rotaciones mediante Lerp y Slerp, lo que permite una reproducción fluida independientemente de la tasa de registro.



Al pasar por cada checkpoint o al completar una vuelta, el jugador puede ver en la UI la diferencia de tiempo respecto al fantasma, calculada a partir de los tiempos almacenados en los checkpoints.



\---



### 1.5 Repetición de carrera



La carrera se guarda para todos los integrantes, incluyendo el jugador y los enemigos controlados por IA.



Cada agente dispone de su propio sistema de grabación de datos, permitiendo reproducir posteriormente la carrera completa con todos los participantes. Tanto la grabación como la reproducción usan la tecnología de los fantasmas.



La repetición puede iniciarse una vez todos los agentes han finalizado la carrera, mediante el botón azul en la pantalla final. Este botón solo se habilita cuando todos los datos de los participantes están disponibles.



Durante la repetición:

Se reinicia el estado de los fantasmas

Se reproducen simultáneamente todos los vehículos

Se utilizan los datos grabados previamente para reconstruir la carrera completa



La cámara sigue al jugador mientras se reproducen todas las vueltas, permitiendo visualizar la carrera desde su perspectiva original.



\---



### 1.6 Visualización del tiempo de vuelta



El tiempo de vuelta se ve constantemente en la parte superior de la pantalla.



Toda la información se muestra en UI y se puede revisar en la script “TimeController”. Esta incluye todos los datos que se muestran en pantalla, incluyendo:



El tiempo constante.

El tiempo por vuelta (esquina superior izquierda).

El tiempo al llegar a un punto de control y al completar una vuelta. Cambiando su color si esta es la mejor marca del jugador en la carrera actual.

El tiempo del fantasma y la diferencia de este respecto al jugador.

El número de vueltas.

# 

