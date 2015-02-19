function onSourceDownloadProgressChanged(sender, eventArgs) {
    sender.findName("textBlock1").Text = Math.round((eventArgs.progress * 100), 0).toString();
    sender.findName("scaleTransform").ScaleX = eventArgs.progress;
}