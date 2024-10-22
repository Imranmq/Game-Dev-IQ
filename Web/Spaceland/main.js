let circles = [
  {
    id: "circle1",
    name: "circle 1",
    axisValues: {
      x: 0,
      y: 0,
      width: 1200,
      height: 600,
    },
    circleAxis: {
      x: 600,
      y: 300,
      z: 0,
      width: 50,
      height: 50,
      exist: false,
    },
  },
  {
    id: "circle2",
    name: "circle 2",
    axisValues: {
      x: 0,
      y: 0,
      width: 1200,
      height: 600,
    },
    circleAxis: {
      x: 900,
      y: 300,
      z: 0,
      width: 50,
      height: 50,
      exist: false,
    },
  },
];
let canvas = {
  width: 1200,
  height: 600,
};

let attributes = {
  speed: 15,
};
let keyMethods = {
  d: { first: 0, second: "x", third: "width", method: moveRight },
  a: { first: 1, second: "x", third: "width", method: moveLeft },
  w: { first: 1, second: "y", third: "height", method: moveUp },
  s: { first: 0, second: "y", third: "height", method: moveDown },
};
function moveRight() {
  moveAxis(keyMethods.d.first, keyMethods.d.second, keyMethods.d.third);
}
function moveLeft() {
  moveAxis(keyMethods.a.first, keyMethods.a.second, keyMethods.a.third);
}
function moveUp() {
  moveAxis(keyMethods.w.first, keyMethods.w.second, keyMethods.w.third);
}
function moveDown() {
  moveAxis(keyMethods.s.first, keyMethods.s.second, keyMethods.s.third);
}

function InitializeCanvas() {
  var c = document.getElementById("myCanvas");
  c.addEventListener("keydown", (e) => {
    onKeyPress(e, 0);
  });
  c.addEventListener("keyup", (e) => {
    onKeyPress(e, 1);
  });
}

function moveAxis(pos, axis, wh) {
  var c = document.getElementById("myCanvas");
  var ctx = c.getContext("2d");
  for (let item of circles) {
    let axisValues = item.axisValues;
    let circleAxis = item.circleAxis;

    if (pos > 0) {
      axisValues[axis] += attributes.speed;
      axisValues[wh] += attributes.speed;
    } else {
      axisValues[axis] -= attributes.speed;
      axisValues[wh] -= attributes.speed;
    }

    if (
      inRageCheck(
        { start: axisValues[axis], end: axisValues[wh] },
        {
          start: circleAxis[axis] - circleAxis[wh],
          end: circleAxis[axis] + circleAxis[wh],
        }
      )
    ) {
      MakeCircle(item);
      circleAxis.exist = true;
    } else if (circleAxis.exist == true) {
      circleAxis.exist = false;

      ctx.clearRect(0, 0, canvas.width, canvas.height);
    }
  }
}

function onKeyPress(e, action) {
  let keyObj = keyMethods[e.key];
  if (action == 0) {
    if (!keyObj.intervalSet) {
      keyObj.intervalSet = setInterval(keyObj.method, 20);
    }
  } else if (action == 1) {
    if (keyObj.intervalSet) {
      clearInterval(keyObj.intervalSet);
      keyObj.intervalSet = null;
    }
  }
}

function MakeCircle(item) {
  var c = document.getElementById("myCanvas");
  var ctx = c.getContext("2d");
  let axisValues = item.axisValues;
  let circleAxis = item.circleAxis;
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  ctx.beginPath();
  ctx.arc(
    axisValues.x + circleAxis.x,
    axisValues.y + circleAxis.y,
    circleAxis.width,
    0,
    2 * Math.PI
  );

  ctx.fillStyle = "#0095DD";
  ctx.fill();
  ctx.stroke();
  ctx.closePath();
}

function inRageCheck(source, target) {
  let tStartInSrcRange =
    source.start < target.start && source.end > target.start;
  let tEndInSrcRange = source.start < target.end && source.end > target.end;
  return tStartInSrcRange || tEndInSrcRange;
}
