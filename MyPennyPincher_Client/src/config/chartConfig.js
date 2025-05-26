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
