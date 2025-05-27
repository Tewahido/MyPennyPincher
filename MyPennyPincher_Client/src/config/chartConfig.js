export const barOptions = {
  responsive: true,
  layout: {
    padding: 5,
  },
  scales: {
    y: {
      beginAtZero: true,
      ticks: {
        stepSize: 500,
      },
    },
  },
  legend: {
    position: "bottom",
  },
};

export const donutOptions = {
  responsive: true,
  cutout: "40%",
  layout: {
    padding: 5,
  },
  plugins: {
    legend: {
      position: "bottom",
      labels: {
        font: {
          size: 16,
        },
      },
    },
  },
};

export const lineOptions = {
  responsive: true,
  layout: {
    padding: 5,
  },
  plugins: {
    legend: {
      display: false,
    },
    title: {
      display: false,
    },
  },
};

export const categoryColours = [
  "rgba(34, 139, 34, 1)",
  "rgba(255, 99, 132, 1)",
  "rgba(54, 162, 235, 1)",
  "rgba(255, 206, 86, 1)",
  "rgba(75, 192, 192, 1)",
  "rgba(153, 102, 255, 1)",
  "rgba(255, 159, 64, 1)",
  "rgba(199, 199, 199, 1)",
  "rgba(83, 102, 255, 1)",
  "rgba(255, 102, 255, 1)",
  "rgba(102, 255, 178, 1)",
  "rgba(255, 204, 204, 1)",
  "rgba(204, 255, 229, 1)",
  "rgba(255, 255, 153, 1)",
  "rgba(102, 153, 255, 1)",
];
