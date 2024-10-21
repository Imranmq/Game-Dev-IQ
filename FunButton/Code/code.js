var bracketsArray = [
  100,
  150,
  225,
  338,
  506,
  759,
  1139,
  1709,
  2563,
  3844,
  5767,
  8650,
  12975,
  19462,
  29193,
  43789,
  65684,
  98526,
  147789,
  221684,
  332526,
  498789,
  748183,
  1122274,
  1683411,
  2525117,
  3787675,
  5681513
];

var multiplierArray = [
  1.15,
  1.16,
  1.17,
  1.18,
  1.2,
  1.21,
  1.22,
  1.23,
  1.25,
  1.26,
  1.27,
  1.28,
  1.3,
  1.31,
  1.32,
  1.34,
  1.35,
  1.36,
  1.38,
  1.39,
  1.4,
  1.42,
  1.43,
  1.45,
  1.46,
  1.47,
  1.49,
  1.5
];

var maxStamina = [100];
completed = false;
var index = 0;
function callIT() {
  while (completed == false) {
    for (var bIndex = 0; bIndex < bracketsArray.length; bIndex++) {
      if (maxStamina[index] <= bracketsArray[bIndex]) {
        var newStaminaValue =
          maxStamina[index] *
          (multiplierArray[bIndex - 1]
            ? multiplierArray[bIndex - 1]
            : multiplierArray[bIndex]);
        maxStamina.push(newStaminaValue);

        break;
      }
    }
    index++;
    if (index > 1000) {
      completed = true;
    }
  }

  maxStamina.forEach(element => {
    var listElement = document.createElement("div");
    listElement.innerHTML = element;
    document.getElementById("toPutData").appendChild = listElement;
  });
  console.log(document.getElementById("toPutData").innerHTML);
}
