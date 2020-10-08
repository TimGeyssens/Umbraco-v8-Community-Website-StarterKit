module.exports = {
  future: {
    removeDeprecatedGapUtilities: true,
    purgeLayersByDefault: true,
  },
  purge: [],
  theme: {
    extend: {
      colors: {
        primary: {
          '100': '#5A64FC',
          '200': '#3C46E6',
          '300': '#1923BE',
        },
        secondary: {
          '100': '#FF6B17',
          '200': '#FF5C00',
          '300': '#E75C0D',
        }
      }
    }
  },
  variants: {
    gradientColorStops: ['responsive', 'hover', 'focus', 'active'],
  },
  plugins: [],
}