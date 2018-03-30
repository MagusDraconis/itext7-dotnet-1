using iText.StyledXmlParser.Node;
using iText.Svg.Renderers;

namespace iText.Svg.Renderers.Factories {
    /// <summary>
    /// Interface for the factory used by
    /// <see cref="iText.Svg.Processors.Impl.DefaultSvgProcessor"/>
    /// .
    /// Pass along using
    /// <see cref="iText.Svg.Processors.ISvgConverterProperties"/>
    /// .
    /// </summary>
    public interface ISvgNodeRendererFactory {
        /// <summary>Create a configured renderer based on the passed Svg tag and set its parent.</summary>
        /// <param name="tag">Representation of the Svg tag, with all style attributes set</param>
        /// <param name="parent">renderer of the parent tag</param>
        /// <returns>Configured ISvgNodeRenderer</returns>
        ISvgNodeRenderer CreateSvgNodeRendererForTag(IElementNode tag, ISvgNodeRenderer parent);

        /// <summary>Checks whether the provided tag is an ignored tag of this factory or not.</summary>
        /// <remarks>Checks whether the provided tag is an ignored tag of this factory or not. If ignored, the factory won't process this IElementNode into an ISvgNodeRenderer.
        ///     </remarks>
        /// <param name="tag">the IElementNode</param>
        /// <returns>true if ignored</returns>
        bool IsTagIgnored(IElementNode tag);
    }
}
