// Karma configuration for Shop_Ez (Jasmine)
module.exports = function (config) {
  config.set({
    basePath: '',
    frameworks: ['jasmine'],
    plugins: [
      require('karma-jasmine'),
      require('karma-chrome-launcher'),
      require('karma-jasmine-html-reporter'),
      require('karma-coverage')
    ],
    client: {
      jasmine: {
        random: false
      },
      clearContext: false
    },
    jasmineHtmlReporter: {
      suppressAll: true
    },
    coverageReporter: {
      dir: require('path').join(__dirname, './coverage/Shop_Ez'),
      subdir: '.',
      reporters: [{ type: 'html' }, { type: 'text-summary' }]
    },
    reporters: ['progress', 'kjhtml'],
    browsers: ['ChromeHeadless'],
    customLaunchers: {
      ChromeVisible: {
        base: 'Chrome',
        flags: [
          '--window-size=1280,800',
          '--window-position=50,50',
          '--disable-background-timer-throttling',
          '--disable-renderer-backgrounding'
        ]
      }
    },
    restartOnFileChange: true,
    // false = keep Karma (and browser) open until you press Ctrl+C in the terminal
    singleRun: false,
    autoWatch: true
  });
};
